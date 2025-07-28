using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiBiblio.Database;
using ApiBiblio.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiBiblio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuteursEndPoints : ControllerBase
    {
        private readonly BiblioDb _context;

        public AuteursEndPoints(BiblioDb context)
        {
            _context = context;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Récupère la liste de tous les auteurs")]
        [SwaggerResponse(200, "Succès", typeof(IEnumerable<Auteur>))]
        public async Task<ActionResult<IEnumerable<Auteur>>> GetAuteurs()
        {
            return await _context.Auteurs.ToListAsync();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Récupère un auteur par son ID")]
        [SwaggerResponse(200, "Succès", typeof(Auteur))]
        [SwaggerResponse(404, "Auteur non trouvé")]
        public async Task<ActionResult<Auteur>> GetAuteur(int id)
        {
            var auteur = await _context.Auteurs.FindAsync(id);

            if (auteur == null)
            {
                return NotFound();
            }

            return auteur;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crée un nouvel auteur")]
        [SwaggerResponse(201, "Auteur créé avec succès", typeof(Auteur))]
        public async Task<ActionResult<Auteur>> PostAuteur(Auteur auteur)
        {
            _context.Auteurs.Add(auteur);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuteur), new { id = auteur.Id }, auteur);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Met à jour un auteur existant")]
        [SwaggerResponse(204, "Mise à jour réussie")]
        [SwaggerResponse(400, "Mauvaise requête")]
        [SwaggerResponse(404, "Auteur non trouvé")]
        public async Task<IActionResult> PutAuteur(int id, Auteur auteur)
        {
            if (id != auteur.Id)
                return BadRequest();

            _context.Entry(auteur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Auteurs.Any(e => e.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Supprime un auteur")]
        [SwaggerResponse(204, "Suppression réussie")]
        [SwaggerResponse(404, "Auteur non trouvé")]
        public async Task<IActionResult> DeleteAuteur(int id)
        {
            var auteur = await _context.Auteurs.FindAsync(id);
            if (auteur == null)
                return NotFound();

            _context.Auteurs.Remove(auteur);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

