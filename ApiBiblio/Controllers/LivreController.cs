using ApiBiblio.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBiblio.Controllers
{
    [Authorize]
    public class LivreController : Controller
    {
        private readonly ILivreService _livreService;

        public LivreController(ILivreService livreService)
        {
            _livreService = livreService;
        }

        public async Task<IActionResult> Index()
        {
            var livres = await _livreService.GetAllAsync();
            return View(livres);
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var livre = await _livreService.GetByIdAsync(id);
                return View(livre);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Administrateur,Bibliothécaire")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrateur,Bibliothécaire")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var livre = await _livreService.GetByIdAsync(id);
                return View(livre);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var livre = await _livreService.GetByIdAsync(id);
                return View(livre);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
