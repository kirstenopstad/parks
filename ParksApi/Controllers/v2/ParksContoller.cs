using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParksApi.Models;

namespace ParksApi.Controllers.v2;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
[ApiController]
public class ParksController : ControllerBase
{
  private readonly ParksApiContext _db;

  public ParksController(ParksApiContext db)
  {
    _db = db;
  }

  // READ
  // GET (list) api/parks
  [HttpGet]
  // async function returning an action result that returns an enumerable collection of type Park called Get()
  public async Task<ActionResult<IEnumerable<Park>>> Get(int parkType, string state, string city)
  {
    IQueryable<Park> query = _db.Parks.AsQueryable();
    // Query by park type (national / state)
    if (parkType != 0)
    {
      query = query.Where(park => park.ParkTypeId == parkType);
    }
    // Query by State
    if (state != null)
    {
      query = query.Where(park => park.State == state);
    }
    // Query by City
    if (city != null)
    {
      query = query.Where(park => park.City == city);
    }
    return await query.ToListAsync();
  }

  // GET (single) api/parks/{id}
  [HttpGet("{id}")]
  public async Task<ActionResult<Park>> GetPark(int id)
  {
    Park park = await _db.Parks.FindAsync(id);

    // return not found if query returns null
    if (park == null)
    {
      return NotFound();
    }

    return park;
  }

  // CREATE
  // POST api/parks
  [HttpPost]
  public async Task<ActionResult<Park>> Post(Park park)
  {
    _db.Parks.Add(park);
    await _db.SaveChangesAsync();
    return CreatedAtAction(nameof(GetPark), new { id = park.ParkId }, park);
  }

  // UPDATE
  // PUT (OR PATCH) api/parks/{id}
  [HttpPut("{id}")]
  public async Task<ActionResult> Put(int id, Park park)
  {
    // ensure URL id matches park being updated
    if (id != park.ParkId)
    {
      return BadRequest();
    }
    // Update entity
    _db.Parks.Update(park);
    // TRY save changes to db
    try
    {
      await _db.SaveChangesAsync();
    }
    // CATCH 
    catch
    {
      if (!ParkExists(id))
      {
        // if park doesn't exist, return not found
        return NotFound();
      }
      else
      {
        // else throw
        throw;
      }
    }
    // return no content
    return NoContent();
  }

  // DELETE
  // DELETE api/parks/{id}
  [HttpDelete("{id}")]
  public async Task<ActionResult> Delete(int id)
  {
    // look up park based on id
    Park park = await _db.Parks.FindAsync(id);
    // remove from parks entity
    _db.Parks.Remove(park);
    // async save changes
    await _db.SaveChangesAsync();
    // return no content
    return NoContent();
  }

  // Helper functions
  private bool ParkExists(int id)
  {
    return _db.Parks.Any(park => park.ParkId == id);
  } 
}
