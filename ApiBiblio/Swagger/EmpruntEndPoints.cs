using ApiBiblio.Database;
using ApiBiblio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblio.Swagger
{
    public class EmpruntEndPoints : ControllerBase
    {
        private readonly BiblioDb _context;

        public EmpruntEndPoints(BiblioDb context)
        {
            _context = context;
        }

        // GET: api/EmpruntsEndPoints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emprunt>>> GetEmprunt()
        {
            return await _context.Emprunts.ToListAsync();
        }

        // GET: api/EmpruntsEndPoints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Emprunt>> GetEmprunt(int id)
        {
            var emprunt = await _context.Emprunts.FindAsync(id);

            if (emprunt == null)
            {
                return NotFound();
            }

            return emprunt;
        }

        // POST: api/EmpruntsEndPoints
        [HttpPost]
        public async Task<ActionResult<Emprunt>> PostEmprunt(Emprunt emprunt)
        {
            _context.Emprunts.Add(emprunt);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmprunt), new { id = emprunt.Id }, emprunt);
        }

