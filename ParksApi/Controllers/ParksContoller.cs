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
    return await _db.Parks.ToListAsync();
  }
  // GET (single) api/parks/{id}
  [HttpGet("{id}")]

  // CREATE
  // POST api/parks
  [HttpPost]

  // UPDATE
  // PUT (OR PATCH) api/parks/{id}
  [HttpPut("{id}")]

  // DELETE
  // DELETE api/parks/{id}
  [HttpDelete("{id}")]
  
}