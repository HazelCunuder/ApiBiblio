using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiBiblio.Database;
using ApiBiblio.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiBiblio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesEndPoints : ControllerBase
    {
        private readonly BiblioDb _context;

        public CategoriesEndPoints(BiblioDb context)
        {
            _context = context;
        }

        // GET: api/CategoriesEndPoints
        [HttpGet]
        [SwaggerOperation(Summary = "Récupère la liste de toutes les catégories")]
        [SwaggerResponse(200, "Succès", typeof(IEnumerable<Categorie>))]
        public async Task<ActionResult<IEnumerable<Categorie>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/CategoriesEndPoints/id
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Récupère une catégorie par son ID")]
        [SwaggerResponse(200, "Succès", typeof(Categorie))]
        [SwaggerResponse(404, "Catégorie non trouvée")]
        public async Task<ActionResult<Categorie>> GetCategorie(int id)
        {
            var categorie = await _context.Categories.FindAsync(id);

            if (categorie == null)
            {
                return NotFound();
            }

            return categorie;
        }

        // POST: api/CategoriesEndPoints
        [HttpPost]
        [SwaggerOperation(Summary = "Crée une nouvelle catégorie")]
        [SwaggerResponse(201, "Catégorie créée avec succès", typeof(Categorie))]
        public async Task<ActionResult<Categorie>> PostCategorie(Categorie categorie)
        {
            _context.Categories.Add(categorie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategorie), new { id = categorie.Id }, categorie);
        }

        // PUT: api/CategoriesEndPoints/5
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Met à jour une catégorie existante")]
        [SwaggerResponse(204, "Mise à jour réussie")]
        [SwaggerResponse(400, "Mauvaise requête")]
        [SwaggerResponse(404, "Catégorie non trouvée")]
        public async Task<IActionResult> PutCategorie(int id, Categorie categorie)
        {
            if (id != categorie.Id)
            {
                return BadRequest();
            }

            _context.Entry(categorie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategorieExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/CategoriesEndPoints/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Supprime une catégorie")]
        [SwaggerResponse(204, "Suppression réussie")]
        [SwaggerResponse(404, "Catégorie non trouvée")]
        public async Task<IActionResult> DeleteCategorie(int id)
        {
            var categorie = await _context.Categories.FindAsync(id);
            if (categorie == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categorie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategorieExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
