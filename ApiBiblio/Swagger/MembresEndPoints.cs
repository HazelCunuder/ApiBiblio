using ApiBiblio.Database;
using ApiBiblio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;


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
        [SwaggerOperation(Summary = "Liste tous les membres", Description = "Récupère la liste complète des membres.")]
        [SwaggerResponse(200, "Succès", typeof(IEnumerable<Membre>))]
        public async Task<ActionResult<IEnumerable<Membre>>> GetMembre()
        {
            return await _context.Membres.ToListAsync();
        }
    
        // GET: api/MembresEndPoints/5
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Récupère un membre", Description = "Récupère un membre à partir de son identifiant.")]
        [SwaggerResponse(200, "Membre trouvé", typeof(Membre))]
        [SwaggerResponse(404, "Membre non trouvé")]
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
        [SwaggerOperation(Summary = "Ajoute un membre", Description = "Ajoute un nouveau membre à la base de données.")]
        [SwaggerResponse(201, "Membre créé", typeof(Membre))]
        public async Task<ActionResult<Membre>> PostMembre(Membre membre)
        {
            _context.Membres.Add(membre);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMembre), new { id = membre.Id }, membre);
        }

        // PUT: api/MembresEndPoints/5
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Modifie un membre", Description = "Met à jour les informations d’un membre existant.")]
        [SwaggerResponse(204, "Membre mis à jour")]
        [SwaggerResponse(400, "ID incohérent")]
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
        [SwaggerOperation(Summary = "Supprime un membre", Description = "Supprime un membre par son identifiant.")]
        [SwaggerResponse(204, "Membre supprimé")]
        [SwaggerResponse(404, "Membre non trouvé")]
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