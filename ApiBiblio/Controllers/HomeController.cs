using ApiBiblio.Database;
using ApiBiblio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ApiBiblio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BiblioDb _context;

        public HomeController(ILogger<HomeController> logger, BiblioDb context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            // Statistiques simples
            var totalLivres = await _context.Livres.CountAsync();
            var livresDisponibles = await _context.Livres.CountAsync(l => l.Disponible);
            var empruntsEnCours = await _context.Emprunts.CountAsync(e => e.DateRetour == null);
            var membresActifs = await _context.Membres.CountAsync();

            // Emprunts récents (on prend les 5 derniers)
            var empruntsRecents = await _context.Emprunts
                .Include(e => e.Membre)
                .Include(e => e.Livre)
                .OrderByDescending(e => e.DateEmprunt)
                .Take(5)
                .Select(e => new
                {
                    Date = e.DateEmprunt,
                    Membre = e.Membre != null ? e.Membre.PrenomMembre + " " + e.Membre.NomMembre : "",
                    Livre = e.Livre != null ? e.Livre.Titre : "",
                    Statut = e.DateRetour == null ? "En cours" : "Terminé"
                })
                .ToListAsync();

            ViewBag.TotalLivres = totalLivres;
            ViewBag.LivresDisponibles = livresDisponibles;
            ViewBag.EmpruntsEnCours = empruntsEnCours;
            ViewBag.MembresActifs = membresActifs;
            ViewBag.EmpruntsRecents = empruntsRecents;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
