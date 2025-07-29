using Microsoft.AspNetCore.Mvc;
using ApiBiblio.Database;
using ApiBiblio.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblio.Controllers
{
    public class AccountController : Controller
    {
        private readonly BiblioDb _context;

        public AccountController(BiblioDb context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string loginEmploye, string mdpEmploye)
        {
            var employe = await _context.Employes.FirstOrDefaultAsync(e => e.LoginEmploye == loginEmploye);

            if (employe == null)
            {
                ViewBag.Error = "Login inconnu";
                return View();
            }

            var hasher = new PasswordHasher<Employe>();
            var result = hasher.VerifyHashedPassword(employe, employe.MdpEmploye, mdpEmploye);

            if (result == PasswordVerificationResult.Success)
            {
                // Récupère le rôle
                var assignation = await _context.AssignerRole.FirstOrDefaultAsync(a => a.EmployeId == employe.Id);
                var role = "Employe";
                if (assignation != null)
                {
                    var r = await _context.Roles.FirstOrDefaultAsync(x => x.Id == assignation.RoleId);
                    if (r != null) role = r.NomRole;
                }

                // Crée l'identité et connecte l'utilisateur AVEC le prénom en ClaimTypes.Name
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, employe.PrenomEmploye), // Affiche le prénom dans le menu !
                    new Claim(ClaimTypes.NameIdentifier, employe.Id.ToString()),
                    new Claim(ClaimTypes.Role, role)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Mauvais mot de passe";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string nomEmploye, string prenomEmploye, string loginEmploye, string mdpEmploye)
        {
            // Vérifie si ce login existe déjà
            if (await _context.Employes.AnyAsync(e => e.LoginEmploye == loginEmploye))
            {
                ViewBag.Error = "Login déjà utilisé.";
                return View();
            }

            var hasher = new PasswordHasher<Employe>();
            var employe = new Employe
            {
                NomEmploye = nomEmploye,
                PrenomEmploye = prenomEmploye,
                LoginEmploye = loginEmploye,
                MdpEmploye = hasher.HashPassword(null!, mdpEmploye)
            };

            _context.Employes.Add(employe);
            await _context.SaveChangesAsync();

            // --- Assigner automatiquement le rôle Admin à ce nouvel employé ---
            var adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.NomRole == "Admin");
            if (adminRole != null)
            {
                _context.AssignerRole.Add(new AssignerRole { RoleId = adminRole.Id, EmployeId = employe.Id });
                await _context.SaveChangesAsync();
            }
            // ---------------------------------------------------------------

            ViewBag.Message = "Employé créé avec succès !";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
