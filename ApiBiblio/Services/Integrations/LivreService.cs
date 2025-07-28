using Microsoft.EntityFrameworkCore;
using ApiBiblio.Database;
using ApiBiblio.Models;
using ApiBiblio.Services.Interfaces;
using static ApiBiblio.DTOs.LivreDTO;

namespace ApiBiblio.Services.Integrations
{
    public class LivreService : ILivreService
    {
        private readonly BiblioDb _context;

        public LivreService(BiblioDb context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LivreDto>> GetAllAsync()
        {
            var livres = await _context.Livres
                .Include(l => l.Auteur)
                .Include(l => l.Categorie)
                .Include(l => l.Genre)
                .ToListAsync();

            return livres.Select(l => new LivreDto
            {
                Id = l.Id,
                Titre = l.Titre,
                Disponible = l.Disponible,
                AnneePublication = l.AnnePublication,
                Isbn = l.ISBN,
                Categorie = l.Categorie.NomCategorie,
                Auteur = $"{l.Auteur.PrenomAuteur} {l.Auteur.NomAuteur}",
                Genre = l.Genre.NomGenre
            });
        }

        public async Task<LivreDto> GetByIdAsync(int id)
        {
            var livre = await _context.Livres
                .Include(l => l.Auteur)
                .Include(l => l.Categorie)
                .Include(l => l.Genre)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (livre == null)
                throw new KeyNotFoundException($"Livre avec l'ID {id} non trouvé");

            return new LivreDto
            {
                Id = livre.Id,
                Titre = livre.Titre,
                Disponible = livre.Disponible,
                AnneePublication = livre.AnnePublication,
                Isbn = livre.ISBN,
                Categorie = livre.Categorie.NomCategorie,
                Auteur = $"{livre.Auteur.PrenomAuteur} {livre.Auteur.NomAuteur}",
                Genre = livre.Genre.NomGenre
            };
        }

        public async Task<LivreDto> CreateAsync(CreateLivreDto createLivreDto)
        {
            // Vérifier si l'ISBN existe déjà
            var existingLivre = await _context.Livres
                .FirstOrDefaultAsync(l => l.ISBN == createLivreDto.Isbn);

            if (existingLivre != null)
                throw new InvalidOperationException("Un livre avec cet ISBN existe déjà");

            var livre = new Livre
            {
                Titre = createLivreDto.Titre,
                AnnePublication = createLivreDto.AnneePublication,
                ISBN = createLivreDto.Isbn,
                IdCategorie = createLivreDto.IdCategorie,
                IdAuteur = createLivreDto.IdAuteur,
                IdGenre = createLivreDto.IdGenre,
                Disponible = true
            };

            _context.Livres.Add(livre);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(livre.Id);
        }

        public async Task<LivreDto> UpdateAsync(int id, UpdateLivreDto updateLivreDto)
        {
            var livre = await _context.Livres.FindAsync(id);
            if (livre == null)
                throw new KeyNotFoundException($"Livre avec l'ID {id} non trouvé");

            livre.Titre = updateLivreDto.Titre;
            livre.AnnePublication = updateLivreDto.AnneePublication;
            livre.IdCategorie = updateLivreDto.IdCategorie;
            livre.IdAuteur = updateLivreDto.IdAuteur;
            livre.IdGenre = updateLivreDto.IdGenre;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var livre = await _context.Livres.FindAsync(id);
            if (livre == null)
                return false;

            // Vérifier si le livre est emprunté
            var isEmprunte = await _context.EmpruntLivres
                .AnyAsync(el => el.IdLivre == id &&
                               el.Emprunt.Statut == "En cours");

            if (isEmprunte)
                throw new InvalidOperationException("Impossible de supprimer un livre actuellement emprunté");

            _context.Livres.Remove(livre);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<LivreDto>> GetAvailableAsync()
        {
            var livres = await _context.Livres
                .Where(l => l.Disponible)
                .Include(l => l.Auteur)
                .Include(l => l.Categorie)
                .Include(l => l.Genre)
                .ToListAsync();

            return livres.Select(l => new LivreDto
            {
                Id = l.Id,
                Titre = l.Titre,
                Disponible = l.Disponible,
                AnneePublication = l.AnnePublication,
                Isbn = l.ISBN,
                Categorie = l.Categorie.NomCategorie,
                Auteur = $"{l.Auteur.PrenomAuteur} {l.Auteur.NomAuteur}",
                Genre = l.Genre.NomGenre
            });
        }

        public async Task<bool> UpdateAvailabilityAsync(int id, bool disponible)
        {
            var livre = await _context.Livres.FindAsync(id);
            if (livre == null)
                return false;

            livre.Disponible = disponible;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
