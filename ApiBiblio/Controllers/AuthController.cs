using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiBiblio.Models;
using ApiBiblio.Database;
using Microsoft.AspNetCore.Identity;

namespace ApiBiblio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly BiblioDb _context;
        private readonly IConfiguration _config;

        public AuthController(BiblioDb context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            // Recherche de l'employé par login
            var employe = await _context.Employes.FirstOrDefaultAsync(e => e.LoginEmploye == login.LoginEmploye);
            if (employe == null)
                return Unauthorized("Utilisateur inconnu.");

            // Vérification du mot de passe hashé
            var hasher = new PasswordHasher<Employe>();
            var result = hasher.VerifyHashedPassword(employe, employe.MdpEmploye, login.MdpEmploye);

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Mot de passe incorrect.");

            // Récupération du rôle de l'employé via la table Assigner
            var assignation = await _context.Assigner
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.IdEmploye == employe.Id);

            string roleName = assignation?.Role?.NomRole ?? "Employe";

            // Création des claims pour le JWT
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, employe.Id.ToString()),
                new Claim(ClaimTypes.Name, employe.LoginEmploye),
                new Claim(ClaimTypes.Role, roleName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                role = roleName
            });
        }
    }
}
