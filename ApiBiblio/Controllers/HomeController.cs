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
            var stats = new DashboardStats
            {
                LivreCount = await _context.Livres.CountAsync(),
                LivreDispoCount = await _context.Livres.CountAsync(l => l.Disponible),
                EmpruntEnCoursCount = await _context.Emprunts.CountAsync(e => e.DateRetour == null),
                MembreCount = await _context.Membres.CountAsync(),
                EmpruntsRecents = await _context.Emprunts
                    .OrderByDescending(e => e.DateEmprunt)
                    .Include(e => e.Membre)
                    .Include(e => e.Livre)
                    .Take(5)
                    .ToListAsync()
            };

            return View(stats);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
