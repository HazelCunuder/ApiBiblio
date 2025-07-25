using ApiBiblio.Database;
using ApiBiblio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployesEndPoints : ControllerBase
    {
        private readonly BiblioDb _context;

        public EmployesEndPoints(BiblioDb context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employe>>> GetEmploye()
        {
            return await _context.Employes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employe>> GetEmploye(int id)
        {
            var employe = await _context.Employes.FindAsync(id);

            if (employe == null)
            {
                return NotFound();
            }

            return employe;
        }

        [HttpPost]
        public async Task<ActionResult<Employe>> PostEmploye(Employe employe)
        {
            _context.Employes.Add(employe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmploye), new { id = employe.Id }, employe);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmploye(int id, Employe employe)
        {
            if (id != employe.Id)
            {
                return BadRequest();
            }

            _context.Entry(employe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Employes.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmploye(int id)
        {
            var employe = await _context.Employes.FindAsync(id);
            if (employe == null)
            {
                return NotFound();
            }

            _context.Employes.Remove(employe);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

