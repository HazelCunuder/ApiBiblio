using ApiBiblio.Database;
using ApiBiblio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembreEndPoints : ControllerBase
    {
        private readonly BiblioDb _context;

        public MembreEndPoints(BiblioDb context)
        {
            _context = context;
        }

        // GET: api/MembresEndPoints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Membre>>> GetMembre()
        {
            return await _context.Membres.ToListAsync();
        }

        // GET: api/MembresEndPoints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Membre>> GetMembre(int id)
        {
            var membre = await _context.Membres.FindAsync(id);

            if (membre == null)
            {
                return NotFound();
            }

            return membre;
        }

        // POST: api/MembresEndPoints
        [HttpPost]
        public async Task<ActionResult<Membre>> PostMembre(Membre membre)
        {
            _context.Membres.Add(membre);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMembre), new { id = membre.Id }, membre);
        }

        // PUT: api/MembresEndPoints/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMembre(int id, Membre membre)
        {
            if (id != membre.Id)
            {
                return BadRequest();
            }

            _context.Entry(membre).State = EntityState.Modified;

            return NoContent();
        }

        // DELETE: api/MembresEndPoints/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMembre(int id)
        {
            var membre = await _context.Membres.FindAsync(id);
            if (membre == null)
            {
                return NotFound();
            }

            _context.Membres.Remove(membre);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}