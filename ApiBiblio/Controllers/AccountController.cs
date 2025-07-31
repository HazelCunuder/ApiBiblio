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
            // Initialise ViewBag.Error et ViewBag.Message pour éviter les NullReferenceException au premier chargement
            ViewBag.Error = null;
            ViewBag.Message = null;
            return View();
        }

        // Traite la soumission du formulaire de connexion (POST) - UNIFIÉ pour Employés ET Membres
        [HttpPost]
        public async Task<IActionResult> Login(string loginEmploye, string mdpEmploye)
        {
            // 1. D'abord, recherche parmi les employés par login
            var employe = await _context.Employes.FirstOrDefaultAsync(e => e.LoginEmploye == loginEmploye);

            if (employe != null)
            {
                // Vérifie le mot de passe de l'employé
                var hasherEmploye = new PasswordHasher<Employe>();
                var resultEmploye = hasherEmploye.VerifyHashedPassword(employe, employe.MdpEmploye, mdpEmploye);

                if (resultEmploye == PasswordVerificationResult.Success)
                {
                    // Récupère le rôle associé à l'employé
                    var assignation = await _context.AssignerRole.FirstOrDefaultAsync(a => a.EmployeId == employe.Id);
                    var role = "Employe"; // Rôle par défaut si non trouvé
                    if (assignation != null)
                    {
                        var r = await _context.Roles.FirstOrDefaultAsync(x => x.Id == assignation.RoleId);
                        if (r != null) role = r.NomRole; // Ex : "Admin"
                    }

                    // Crée la liste des claims pour l'employé connecté
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, employe.PrenomEmploye), // Permet d'afficher le prénom dans le menu
                        new Claim(ClaimTypes.NameIdentifier, employe.Id.ToString()), // ID unique de l'employé
                        new Claim(ClaimTypes.Role, role), // Utilisé pour la gestion des accès par rôle
                        new Claim("UserType", "Employe") // Permet de distinguer employé/membre
                    };

                    // Connecte l'employé
                    await ConnectUser(claims);
                    return RedirectToAction("Index", "Home");
                }
            }

            // 2. Si pas trouvé parmi les employés, recherche parmi les membres par email
            var membre = await _context.Membres.FirstOrDefaultAsync(m => m.AdresseMail == loginEmploye);

            if (membre != null)
            {
                // Vérifie le mot de passe du membre
                var hasherMembre = new PasswordHasher<Membre>();
                var resultMembre = hasherMembre.VerifyHashedPassword(membre, membre.MdpMembre, mdpEmploye);

                if (resultMembre == PasswordVerificationResult.Success)
                {
                    // Crée la liste des claims pour le membre connecté
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, membre.PrenomMembre), // Permet d'afficher le prénom dans le menu
                        new Claim(ClaimTypes.NameIdentifier, membre.Id.ToString()), // ID unique du membre
                        new Claim(ClaimTypes.Role, "Membre"), // Rôle fixe pour les membres
                        new Claim("UserType", "Membre") // Permet de distinguer employé/membre
                    };

                    // Connecte le membre
                    await ConnectUser(claims);
                    return RedirectToAction("Index", "Home");
                }
            }

            // 3. Si aucune correspondance trouvée
            ViewBag.Error = "Identifiants incorrects";
            return View();
        }

        // Méthode privée pour éviter la duplication de code de connexion
        private async Task ConnectUser(List<Claim> claims)
        {
            // Crée l'identité et le principal pour l'authentification cookie
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Connecte l'utilisateur (création du cookie d'authentification)
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        // Affiche la page de création d'employé (GET)
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Initialise ViewBag.Error et ViewBag.Message pour éviter les NullReferenceException au premier chargement
            ViewBag.Error = null;
            ViewBag.Message = null;
            // Récupère les rôles à afficher dans la liste déroulante
            ViewBag.Roles = await _context.Roles.ToListAsync();
            return View();
        }

        // Traite la création d'un nouvel employé (POST)
        [HttpPost]
        public async Task<IActionResult> Create(string nomEmploye, string prenomEmploye, string loginEmploye, string mdpEmploye, int roleId)
        {
            // Vérifie si un employé existe déjà avec ce login
            if (await _context.Employes.AnyAsync(e => e.LoginEmploye == loginEmploye))
            {
                ViewBag.Error = "Login déjà utilisé.";
                ViewBag.Roles = await _context.Roles.ToListAsync(); // Re-fetch roles on error
                return View();
            }

            // Hash le mot de passe avant de le stocker
            var hasher = new PasswordHasher<Employe>();
            var employe = new Employe
            {
                NomEmploye = nomEmploye,
                PrenomEmploye = prenomEmploye,
                LoginEmploye = loginEmploye,
                MdpEmploye = hasher.HashPassword(null!, mdpEmploye),
                IdRole = roleId
            };

            // Ajoute le nouvel employé à la base
            _context.Employes.Add(employe);
            await _context.SaveChangesAsync();

            // Assigne le rôle sélectionné à ce nouvel employé
            var selectedRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
            if (selectedRole != null)
            {
                _context.AssignerRole.Add(new AssignerRole { RoleId = selectedRole.Id, EmployeId = employe.Id });
                await _context.SaveChangesAsync();
            }
            else
            {
                // Gère le cas où aucun rôle ou un rôle invalide est sélectionné (par exemple, attribuer un rôle par défaut)
                ViewBag.Error = "Rôle sélectionné invalide. Veuillez réessayer.";
                ViewBag.Roles = await _context.Roles.ToListAsync(); // Re-fetch roles on error
                return View();
            }

            ViewBag.Message = "Compte créé avec succès !";
            ViewBag.Roles = await _context.Roles.ToListAsync(); // Re-fetch roles for a fresh view
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