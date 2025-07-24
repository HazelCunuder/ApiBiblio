using ApiBiblio.Database;
using ApiBiblio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblio.Swagger
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivresEndPoints : ControllerBase
    {
        private readonly BiblioDb _context;

        public LivresEndPoints(BiblioDb context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livre>>> GetLivres()
        {
            return Ok(await _context.Livres.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Livre>> GetLivre(int id)
        {
            var livre = await _context.Livres.FindAsync(id);
            if (livre == null) return NotFound();
            return Ok(livre);
        }

        [HttpPost]
        public async Task<ActionResult<Livre>> PostLivre(Livre livre)
        {
            _context.Livres.Add(livre);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLivre), new { id = livre.Id }, livre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivre(int id, Livre livre)
        {
            if (id != livre.Id) return BadRequest();

            _context.Entry(livre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivre(int id)
        {
            var livre = await _context.Livres.FindAsync(id);
            if (livre == null) return NotFound();

            _context.Livres.Remove(livre);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

