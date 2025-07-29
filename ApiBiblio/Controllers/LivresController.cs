using ApiBiblio.Database;
using ApiBiblio.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var biblioDb = _context.Livres
                .Include(l => l.Auteur)
                .Include(l => l.Categorie)
                .Include(l => l.Genre)
                .AsNoTracking();
            return View(await biblioDb.ToListAsync());
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
                .Include(l => l.Auteur)
                .Include(l => l.Categorie)
                .Include(l => l.Genre)
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
            ViewData["IdAuteur"] = new SelectList(_context.Auteurs, "Id", "NomAuteur");
            ViewData["IdCategorie"] = new SelectList(_context.Categories, "Id", "NomCategorie");
            ViewData["IdGenre"] = new SelectList(_context.Genres, "Id", "NomGenre");
            return View();
        }

        // POST: Livres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // AJOUT : Seul l'admin peut créer
        public async Task<IActionResult> Create([Bind("Id,IdAuteur,Titre,Disponible,AnnePublication,ISBN,IdCategorie,IdGenre")] Livre livre)
        {
            var formData = HttpContext.Request.Form;
            foreach (var key in formData.Keys)
            {
                Console.WriteLine($"{key} = {formData[key]}");
            }

            ViewData["IdAuteur"] = new SelectList(_context.Auteurs, "Id", "NomAuteur", livre.IdAuteur);
            ViewData["IdCategorie"] = new SelectList(_context.Categories, "Id", "NomCategorie", livre.IdCategorie);
            ViewData["IdGenre"] = new SelectList(_context.Genres, "Id", "NomGenre", livre.IdGenre);
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
            ViewData["IdAuteur"] = new SelectList(_context.Auteurs, "Id", "NomAuteur", livre.IdAuteur);
            ViewData["IdCategorie"] = new SelectList(_context.Categories, "Id", "NomCategorie", livre.IdCategorie);
            ViewData["IdGenre"] = new SelectList(_context.Genres, "Id", "NomGenre", livre.IdGenre);
            return View(livre);
        }

        // POST: Livres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // AJOUT : Seul l'admin peut modifier
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdAuteur,Titre,Disponible,AnnePublication,ISBN,IdCategorie,IdGenre")] Livre livre)
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
            ViewData["IdAuteur"] = new SelectList(_context.Auteurs, "Id", "NomAuteur", livre.IdAuteur);
            ViewData["IdCategorie"] = new SelectList(_context.Categories, "Id", "NomCategorie", livre.IdCategorie);
            ViewData["IdGenre"] = new SelectList(_context.Genres, "Id", "NomGenre", livre.IdGenre);
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
                .Include(l => l.Auteur)
                .Include(l => l.Categorie)
                .Include(l => l.Genre)
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
