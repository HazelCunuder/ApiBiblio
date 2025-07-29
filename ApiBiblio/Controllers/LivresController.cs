using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiBiblio.Database;
using ApiBiblio.Models;
using Microsoft.AspNetCore.Authorization; // AJOUT : Pour les droits

namespace ApiBiblio.Controllers
{
    public class LivresController : Controller
    {
        private readonly BiblioDb _context;

        public LivresController(BiblioDb context)
        {
            _context = context;
        }

        // GET: Livres
        [Authorize] // AJOUT : Lecture accessible à tous les employés connectés
        public async Task<IActionResult> Index()
        {
            return View(await _context.Livres.ToListAsync());
        }

        // GET: Livres/Details/5
        [Authorize] // AJOUT : Lecture accessible à tous les employés connectés
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livre == null)
            {
                return NotFound();
            }

            return View(livre);
        }

        // GET: Livres/Create
        [Authorize(Roles = "Admin")] // AJOUT : Seul l'admin peut accéder à la vue de création
        public IActionResult Create()
        {
            return View();
        }

        // POST: Livres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // AJOUT : Seul l'admin peut créer
        public async Task<IActionResult> Create([Bind("Id,Titre,Disponible,AnnePublication,ISBN,IdCategorie,IdEmprunt")] Livre livre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(livre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(livre);
        }

        // GET: Livres/Edit/5
        [Authorize(Roles = "Admin")] // AJOUT : Seul l'admin peut accéder à la vue d'édition
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres.FindAsync(id);
            if (livre == null)
            {
                return NotFound();
            }
            return View(livre);
        }

        // POST: Livres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // AJOUT : Seul l'admin peut modifier
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titre,Disponible,AnnePublication,ISBN,IdCategorie,IdEmprunt")] Livre livre)
        {
            if (id != livre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivreExists(livre.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(livre);
        }

        // GET: Livres/Delete/5
        [Authorize(Roles = "Admin")] // AJOUT : Seul l'admin peut accéder à la vue de suppression
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livre == null)
            {
                return NotFound();
            }

            return View(livre);
        }

        // POST: Livres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // AJOUT : Seul l'admin peut supprimer
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livre = await _context.Livres.FindAsync(id);
            if (livre != null)
            {
                _context.Livres.Remove(livre);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivreExists(int id)
        {
            return _context.Livres.Any(e => e.Id == id);
        }
    }
}
