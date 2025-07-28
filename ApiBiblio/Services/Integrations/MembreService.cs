using ApiBiblio.Database;
using ApiBiblio.DTOs;
using ApiBiblio.Models;
using ApiBiblio.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Scripting;
using static ApiBiblio.DTOs.MembreDTO;

namespace ApiBiblio.Services.Integrations
{
    public class MembreService : IMembreService
    {
        private readonly BiblioDb _context;

        public MembreService(BiblioDb context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MembreDto>> GetAllAsync()
        {
            var membres = await _context.Membres.ToListAsync();
            return membres.Select(m => new MembreDto
            {
                Id = m.Id,
                NomMembre = m.NomMembre,
                PrenomMembre = m.PrenomMembre,
                AdresseMail = m.AdresseMail,
                Telephone = m.Telephone,
                AdressePostale = m.AdressePostale
            });
        }

        public async Task<MembreDto> GetByIdAsync(int id)
        {
            var membre = await _context.Membres.FindAsync(id);
            if (membre == null)
                throw new KeyNotFoundException($"Membre avec l'ID {id} non trouvé");

            return new MembreDto
            {
                Id = membre.Id,
                NomMembre = membre.NomMembre,
                PrenomMembre = membre.PrenomMembre,
                AdresseMail = membre.AdresseMail,
                Telephone = membre.Telephone,
                AdressePostale = membre.AdressePostale
            };
        }

        public async Task<MembreDto> CreateAsync(CreateMembreDto createMembreDto)
        {
            // Vérifier si l'email existe déjà
            var existingMembre = await _context.Membres
                .FirstOrDefaultAsync(m => m.AdresseMail == createMembreDto.AdresseMail);

            if (existingMembre != null)
                throw new InvalidOperationException("Un membre avec cet email existe déjà");

            // Hasher le mot de passe
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(createMembreDto.MdpMembre);

            var membre = new Membre
            {
                NomMembre = createMembreDto.NomMembre,
                PrenomMembre = createMembreDto.PrenomMembre,
                AdresseMail = createMembreDto.AdresseMail,
                MdpMembre = hashedPassword,
                Telephone = createMembreDto.Telephone,
                AdressePostale = createMembreDto.AdressePostale
            };

            _context.Membres.Add(membre);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(membre.Id);
        }

        public async Task<MembreDto> UpdateAsync(int id, UpdateMembreDto updateMembreDto)
        {
            var membre = await _context.Membres.FindAsync(id);
            if (membre == null)
                throw new KeyNotFoundException($"Membre avec l'ID {id} non trouvé");

            // Vérifier si le nouvel email existe déjà (sauf pour ce membre)
            if (updateMembreDto.AdresseMail != membre.AdresseMail)
            {
                var existingMembre = await _context.Membres
                    .FirstOrDefaultAsync(m => m.AdresseMail == updateMembreDto.AdresseMail);

                if (existingMembre != null)
                    throw new InvalidOperationException("Un membre avec cet email existe déjà");
            }

            membre.NomMembre = updateMembreDto.NomMembre;
            membre.PrenomMembre = updateMembreDto.PrenomMembre;
            membre.AdresseMail = updateMembreDto.AdresseMail;
            membre.Telephone = updateMembreDto.Telephone;
            membre.AdressePostale = updateMembreDto.AdressePostale;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var membre = await _context.Membres.FindAsync(id);
            if (membre == null)
                return false;

            // Vérifier si le membre a des emprunts en cours
            var hasActiveEmprunts = await _context.Emprunts
                .AnyAsync(e => e.IdMembre == id && e.Statut == "En cours");

            if (hasActiveEmprunts)
                throw new InvalidOperationException("Impossible de supprimer un membre ayant des emprunts en cours");

            _context.Membres.Remove(membre);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<EmpruntDTO>> GetHistoriqueAsync(int membreId)
        {
            var emprunts = await _context.Emprunts
                .Where(e => e.IdMembre == membreId)
                .Include(e => e.Membre)
                .Include(e => e.Employe)
                .Include(e => e.EmpruntLivres)
                    .ThenInclude(el => el.Livre)
                .OrderByDescending(e => e.DateEmprunt)
                .ToListAsync();

            return emprunts.Select(e => new EmpruntDTO
            {
                Id = e.Id,
                DateEmprunt = e.DateEmprunt,
                Statut = e.Statut,
                DateRetour = e.DateRetour,
                Membre = $"{e.Membre.PrenomMembre} {e.Membre.NomMembre}",
                Employe = $"{e.Employe.PrenomEmploye} {e.Employe.NomEmploye}",
                Livres = e.EmpruntLivres.Select(el => el.Livre.Titre).ToList()
            });
        }

        public async Task<IEnumerable<EmpruntDTO>> GetEmpruntsEnCoursAsync()
        {
            var emprunts = await _context.Emprunts
                .Where(e => e.Statut == "En cours")
                .Include(e => e.Membre)
                .Include(e => e.Employe)
                .Include(e => e.EmpruntLivres)
                    .ThenInclude(el => el.Livre)
                .OrderByDescending(e => e.DateEmprunt)
                .ToListAsync();

            return emprunts.Select(e => new EmpruntDTO
            {
                Id = e.Id,
                DateEmprunt = e.DateEmprunt,
                Statut = e.Statut,
                DateRetour = e.DateRetour,
                Membre = $"{e.Membre.PrenomMembre} {e.Membre.NomMembre}",
                Employe = $"{e.Employe.PrenomEmploye} {e.Employe.NomEmploye}",
                Livres = e.EmpruntLivres.Select(el => el.Livre.Titre).ToList()
            });
        }
    }
}