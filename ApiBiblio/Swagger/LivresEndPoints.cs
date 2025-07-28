using ApiBiblio.Database;
using ApiBiblio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;


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
        // GET: api/LivresEndPoints
        [HttpGet]
        [SwaggerOperation(Summary = "Liste tous les livres", Description = "Récupère la liste complète des livres disponibles en base.")]
        [SwaggerResponse(200, "Liste des livres", typeof(IEnumerable<Livre>))]
        public async Task<ActionResult<IEnumerable<Livre>>> GetLivres()
        {
            return Ok(await _context.Livres.ToListAsync());
        }

        // GET: api/LivresEndPoints/id
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Récupère un livre", Description = "Récupère un livre en fonction de son identifiant.")]
        [SwaggerResponse(200, "Livre trouvé", typeof(Livre))]
        [SwaggerResponse(404, "Livre non trouvé")]
        public async Task<ActionResult<Livre>> GetLivre(int id)
        {
            var livre = await _context.Livres.FindAsync(id);
            if (livre == null) return NotFound();
            return Ok(livre);
        }

        // POST: api/LivresEndPoints
        [HttpPost]
        [SwaggerOperation(Summary = "Ajoute un nouveau livre", Description = "Crée un nouveau livre dans la base de données.")]
        [SwaggerResponse(201, "Livre créé", typeof(Livre))]
        public async Task<ActionResult<Livre>> PostLivre(Livre livre)
        {
            _context.Livres.Add(livre);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLivre), new { id = livre.Id }, livre);
        }

        // PUT: api/LivresEndPoints/id
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Modifie un livre", Description = "Met à jour les informations d’un livre existant.")]
        [SwaggerResponse(204, "Livre modifié")]
        [SwaggerResponse(400, "Requête invalide (ID incohérent)")]
        public async Task<IActionResult> PutLivre(int id, Livre livre)
        {
            if (id != livre.Id) return BadRequest();

            _context.Entry(livre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/LivresEndPoints/id
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Supprime un livre", Description = "Supprime un livre à partir de son identifiant.")]
        [SwaggerResponse(204, "Livre supprimé")]
        [SwaggerResponse(404, "Livre non trouvé")]
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

