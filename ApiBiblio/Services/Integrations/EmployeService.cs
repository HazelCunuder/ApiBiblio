using Microsoft.EntityFrameworkCore;
using ApiBiblio.Database;
using ApiBiblio.Models;
using ApiBiblio.Services.Interfaces;
using Microsoft.CodeAnalysis.Scripting;
using static ApiBiblio.DTOs.AuthDto;

namespace ApiBiblio.Services.Integrations
{
    public class EmployeService : IEmployeService
    {
        private readonly BiblioDb _context;

        public EmployeService(BiblioDb context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(CreateEmployeDto createEmployeDto)
        {
            // Vérifier si l'email existe déjà
            var existingEmploye = await _context.Employes
                .FirstOrDefaultAsync(e => e.LoginEmploye == createEmployeDto.LoginEmploye);

            if (existingEmploye != null)
                throw new InvalidOperationException("Un employé avec cet email existe déjà");

            // Hasher le mot de passe
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(createEmployeDto.MdpEmploye);

            var employe = new Employe
            {
                NomEmploye = createEmployeDto.NomEmploye,
                PrenomEmploye = createEmployeDto.PrenomEmploye,
                LoginEmploye = createEmployeDto.LoginEmploye,
                MdpEmploye = hashedPassword,
                IdRole = createEmployeDto.IdRole
            };

            _context.Employes.Add(employe);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> ValidateCredentialsAsync(string email, string password)
        {
            var employe = await _context.Employes
                .FirstOrDefaultAsync(e => e.LoginEmploye == email);

            if (employe == null)
                return false;

            return BCrypt.Net.BCrypt.Verify(password, employe.MdpEmploye);
        }

        public async Task<string> GetRoleAsync(string email)
        {
            var employe = await _context.Employes
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.LoginEmploye == email);

            return employe?.Role?.NomRole ?? throw new InvalidOperationException("Employé non trouvé");
        }

        public async Task<int> GetEmployeIdAsync(string email)
        {
            var employe = await _context.Employes
                .FirstOrDefaultAsync(e => e.LoginEmploye == email);

            return employe?.Id ?? throw new InvalidOperationException("Employé non trouvé");
        }
    }
}

