using ApiBiblio.Database;
using ApiBiblio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiBiblio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Nécessite une authentification
    public class EmployesEndPoints : ControllerBase
    {
        private readonly BiblioDb _context;

        public EmployesEndPoints(BiblioDb context)
        {
            _context = context;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Récupère la liste de tous les employés", Description = "Retourne une liste complète des employés.")]
        [SwaggerResponse(200, "Succès", typeof(IEnumerable<Employe>))]
        public async Task<ActionResult<IEnumerable<Employe>>> GetEmploye()
        {
            return await _context.Employes.ToListAsync();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Récupère un employé par son ID", Description = "Retourne l'employé correspondant à l'ID fourni.")]
        [SwaggerResponse(200, "Succès", typeof(Employe))]
        [SwaggerResponse(404, "Employé non trouvé")]
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
        [SwaggerOperation(Summary = "Crée un nouvel employé", Description = "Ajoute un employé à la base de données.")]
        [SwaggerResponse(201, "Employé créé avec succès", typeof(Employe))]
        public async Task<ActionResult<Employe>> PostEmploye(Employe employe)
        {
            _context.Employes.Add(employe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmploye), new { id = employe.Id }, employe);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Met à jour un employé existant", Description = "Met à jour les informations d'un employé.")]
        [SwaggerResponse(204, "Mise à jour réussie")]
        [SwaggerResponse(400, "Mauvaise requête")]
        [SwaggerResponse(404, "Employé non trouvé")]
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
        [SwaggerOperation(Summary = "Supprime un employé", Description = "Supprime un employé selon l'ID fourni.")]
        [SwaggerResponse(204, "Suppression réussie")]
        [SwaggerResponse(404, "Employé non trouvé")]
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

