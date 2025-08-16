using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelloApi4.Data;
using HelloApi4.Models;

namespace HelloApi4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Person>>> GetAll()
        => await db.People.AsNoTracking().ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Person>> GetById(int id)
    {
        var person = await db.People.FindAsync(id);
        return person is null ? NotFound() : Ok(person);
    }

    [HttpPost]
    public async Task<ActionResult<Person>> Create(Person input)
    {
        db.People.Add(input);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = input.Id }, input);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Person input)
    {
        if (id != input.Id) return BadRequest("El id del path no coincide con el del cuerpo.");
        var exists = await db.People.AnyAsync(p => p.Id == id);
        if (!exists) return NotFound();

        db.Entry(input).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var person = await db.People.FindAsync(id);
        if (person is null) return NotFound();

        db.People.Remove(person);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
