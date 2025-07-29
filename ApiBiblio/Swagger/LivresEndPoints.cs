using ApiBiblio.Database;
using ApiBiblio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBiblio.DTOs;

namespace ApiBiblio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivresEndpoints : ControllerBase
    {
        private readonly BiblioDb _context;

        public LivresEndpoints(BiblioDb context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all livres.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livre>>> GetLivres()
        {
            var livres = await _context.Livres
                .Include(l => l.Auteur)
                .Include(l => l.Categorie)
                .Include(l => l.Genre)
                .AsNoTracking()
                .ToListAsync();

            return Ok(livres);
        }

        /// <summary>
        /// Get livre by Id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Livre>> GetLivre(int id)
        {
            var livre = await _context.Livres
                .Include(l => l.Auteur)
                .Include(l => l.Categorie)
                .Include(l => l.Genre)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (livre == null)
            {
                return NotFound();
            }

            return Ok(livre);
        }

        /// <summary>
        /// Create a new livre.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Livre>> CreateLivre([FromBody] Livre livre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Livres.Add(livre);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLivre), new { id = livre.Id }, livre);
        }

        /// <summary>
        /// Update an existing livre.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLivre(int id, [FromBody] Livre livre)
        {
            if (id != livre.Id)
            {
                return BadRequest("ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(livre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Livres.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Delete a livre by Id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivre(int id)
        {
            var livre = await _context.Livres.FindAsync(id);
            if (livre == null)
            {
                return NotFound();
            }

            _context.Livres.Remove(livre);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}