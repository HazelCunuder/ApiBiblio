using ApiBiblio.Database;
using ApiBiblio.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ApiBiblio.Controllers
{
    public class AccountController : Controller
    {
        private readonly BiblioDb _context; // Accès à la base de données

        public AccountController(BiblioDb context)
        {
            _context = context;
        }

        // Affiche la page de connexion (GET)
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Traite la soumission du formulaire de connexion (POST)
        [HttpPost]
        public async Task<IActionResult> Login(string loginEmploye, string mdpEmploye)
        {
            // Recherche l'employé par son login
            var employe = await _context.Employes.FirstOrDefaultAsync(e => e.LoginEmploye == loginEmploye);

            // Si l'utilisateur n'existe pas, affiche un message d'erreur
            if (employe == null)
            {
                ViewBag.Error = "Login inconnu";
                return View();
            }

            // Vérifie si le mot de passe entré correspond au hash stocké
            var hasher = new PasswordHasher<Employe>();
            var result = hasher.VerifyHashedPassword(employe, employe.MdpEmploye, mdpEmploye);

            if (result == PasswordVerificationResult.Success)
            {
                // Récupère le rôle associé à l'employé
                var assignation = await _context.AssignerRole.FirstOrDefaultAsync(a => a.EmployeId == employe.Id);
                var role = "Employe";
                if (assignation != null)
                {
                    var r = await _context.Roles.FirstOrDefaultAsync(x => x.Id == assignation.RoleId);
                    if (r != null) role = r.NomRole; // Ex : "Admin"
                }

                // Crée la liste des claims pour l'utilisateur connecté
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, employe.PrenomEmploye), // Permet d'afficher le prénom dans le menu
                    new Claim(ClaimTypes.NameIdentifier, employe.Id.ToString()), // ID unique de l'employé
                    new Claim(ClaimTypes.Role, role) // Utilisé pour la gestion des accès par rôle
                };

                // Crée l'identité et le principal pour l'authentification cookie
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Connecte l'utilisateur (création du cookie d'authentification)
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Redirige vers la page d'accueil
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Mot de passe incorrect : affiche un message d'erreur
                ViewBag.Error = "Mauvais mot de passe";
                return View();
            }
        }

        // Affiche la page de création d'employé (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Traite la création d'un nouvel employé (POST)
        [HttpPost]
        public async Task<IActionResult> Create(string nomEmploye, string prenomEmploye, string loginEmploye, string mdpEmploye)
        {
            // Vérifie si un employé existe déjà avec ce login
            if (await _context.Employes.AnyAsync(e => e.LoginEmploye == loginEmploye))
            {
                ViewBag.Error = "Login déjà utilisé.";
                return View();
            }

            // Hash le mot de passe avant de le stocker
            var hasher = new PasswordHasher<Employe>();
            var employe = new Employe
            {
                NomEmploye = nomEmploye,
                PrenomEmploye = prenomEmploye,
                LoginEmploye = loginEmploye,
                MdpEmploye = hasher.HashPassword(null!, mdpEmploye)
            };

            // Ajoute le nouvel employé à la base
            _context.Employes.Add(employe);
            await _context.SaveChangesAsync();

            // Assigne automatiquement le rôle Admin à ce nouvel employé (à adapter si besoin)
            var adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.NomRole == "Admin");
            if (adminRole != null)
            {
                _context.AssignerRole.Add(new AssignerRole { RoleId = adminRole.Id, EmployeId = employe.Id });
                await _context.SaveChangesAsync();
            }

            ViewBag.Message = "Employé créé avec succès !";
            return View();
        }

        // Déconnecte l'utilisateur
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Supprime le cookie d'authentification (déconnexion)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
