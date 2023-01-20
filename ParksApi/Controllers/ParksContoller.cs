using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParksApi.Models;

namespace ParksApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParksApiController : ControllerBase
{
  private readonly ParksApiContext _db;

  public ParksApiController(ParksApiContext db)
  {
    _db = db;
  }

  // READ
  // GET (list) api/parks
  [HttpGet]
  // async function returning an action result that returns an enumerable collection of type Park called Get()
  public async Task<ActionResult<IEnumerable<Park>>> Get()
  {
    // TODO: Query by ParkType
    // TODO: Query by State
    // TODO: Query by City
    return await _db.Parks.ToListAsync();
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
      _db.SaveChanges();
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
    // return No Content
    return NoContent();
  }

  // DELETE
  // DELETE api/parks/{id}
  // [HttpDelete("{id}")]

  // Helper functions
  private bool ParkExists(int id)
  {
    return _db.Parks.Any(park => park.ParkId == id);
  } 
}
