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
            if (employe != null)
            {
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

                    // Crée l'identité et connecte l'utilisateur
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, employe.LoginEmploye),
                        new Claim(ClaimTypes.NameIdentifier, employe.Id.ToString()),
                        new Claim(ClaimTypes.Role, role)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.Error = "Identifiants incorrects";
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
