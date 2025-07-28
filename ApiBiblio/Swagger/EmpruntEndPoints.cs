using ApiBiblio.Database;
using ApiBiblio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiBiblio.Swagger
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpruntEndPoints : ControllerBase
    {
        private readonly BiblioDb _context;

        public EmpruntEndPoints(BiblioDb context)
        {
            _context = context;
        }

        // GET: api/EmpruntEndPoints
        [HttpGet]
        [SwaggerOperation(Summary = "Liste tous les emprunts", Description = "Récupère tous les emprunts enregistrés dans la base de données.")]
        [SwaggerResponse(200, "Liste des emprunts", typeof(IEnumerable<Emprunt>))]
        public async Task<ActionResult<IEnumerable<Emprunt>>> GetEmprunt()
        {
            return await _context.Emprunts.ToListAsync();
        }

        // GET: api/EmpruntEndPoints/id
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Récupère un emprunt", Description = "Récupère les détails d’un emprunt par son identifiant.")]
        [SwaggerResponse(200, "Emprunt trouvé", typeof(Emprunt))]
        [SwaggerResponse(404, "Emprunt non trouvé")]
        public async Task<ActionResult<Emprunt>> GetEmprunt(int id)
        {
            var emprunt = await _context.Emprunts.FindAsync(id);

            if (emprunt == null)
            {
                return NotFound();
            }

            return emprunt;
        }

        // POST: api/EmpruntEndPoints
        [HttpPost]
        [SwaggerOperation(Summary = "Ajoute un emprunt", Description = "Crée un nouvel emprunt.")]
        [SwaggerResponse(201, "Emprunt créé", typeof(Emprunt))]
        public async Task<ActionResult<Emprunt>> PostEmprunt(Emprunt emprunt)
        {
            _context.Emprunts.Add(emprunt);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmprunt), new { id = emprunt.Id }, emprunt);
        }

        // PUT: api/EmpruntEndPoints/id
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Modifie un emprunt", Description = "Met à jour les informations d’un emprunt existant.")]
        [SwaggerResponse(204, "Emprunt modifié")]
        [SwaggerResponse(400, "ID incohérent")]
        public async Task<IActionResult> PutEmprunt(int id, Emprunt emprunt)
        {
            if (id != emprunt.Id)
            {
                return BadRequest();
            }

            _context.Entry(emprunt).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/EmpruntEndPoints/id
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Supprime un emprunt", Description = "Supprime un emprunt par son identifiant.")]
        [SwaggerResponse(204, "Emprunt supprimé")]
        [SwaggerResponse(404, "Emprunt non trouvé")]
        public async Task<IActionResult> DeleteEmprunt(int id)
        {
            var emprunt = await _context.Emprunts.FindAsync(id);
            if (emprunt == null)
            {
                return NotFound();
            }

            _context.Emprunts.Remove(emprunt);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
};
