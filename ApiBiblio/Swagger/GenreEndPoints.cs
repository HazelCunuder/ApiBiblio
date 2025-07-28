using ApiBiblio.Database;
using ApiBiblio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;


namespace ApiBiblio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresEndPoints : ControllerBase
    {
        private readonly BiblioDb _context;

        public GenresEndPoints(BiblioDb context)
        {
            _context = context;
        }

        // GET: api/GenresEndPoints
        [HttpGet]
        [SwaggerOperation(Summary = "Liste tous les genres", Description = "Récupère la liste complète des genres disponibles.")]
        [SwaggerResponse(200, "Genres récupérés avec succès", typeof(IEnumerable<Genre>))]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            return await _context.Genres.ToListAsync();
        }

        // GET: api/GenresEndPoints/id
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Récupère un genre", Description = "Récupère un genre par son identifiant.")]
        [SwaggerResponse(200, "Genre trouvé", typeof(Genre))]
        [SwaggerResponse(404, "Genre non trouvé")]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return genre;
        }

        // POST: api/GenresEndPoints
        [HttpPost]
        [SwaggerOperation(Summary = "Ajoute un genre", Description = "Ajoute un nouveau genre à la base de données.")]
        [SwaggerResponse(201, "Genre créé", typeof(Genre))]
        public async Task<ActionResult<Genre>> PostGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, genre);
        }

        // PUT: api/GenresEndPoints/id
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Modifie un genre", Description = "Met à jour un genre existant par son identifiant.")]
        [SwaggerResponse(204, "Genre mis à jour")]
        [SwaggerResponse(400, "ID incohérent")]
        [SwaggerResponse(404, "Genre non trouvé (conflit de mise à jour)")]
        public async Task<IActionResult> PutGenre(int id, Genre genre)
        {
            if (id != genre.Id)
            {
                return BadRequest();
            }

            _context.Entry(genre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Genres.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/GenresEndPoints/id
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Supprime un genre", Description = "Supprime un genre par son identifiant.")]
        [SwaggerResponse(204, "Genre supprimé")]
        [SwaggerResponse(404, "Genre non trouvé")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
