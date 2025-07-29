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
        public IActionResult Create()
        {
            ViewData["IdAuteur"] = new SelectList(_context.Auteurs, "Id", "NomAuteur");
            ViewData["IdCategorie"] = new SelectList(_context.Categories, "Id", "NomCategorie");
            ViewData["IdGenre"] = new SelectList(_context.Genres, "Id", "NomGenre");
            return View();
        }

        // POST: Livres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdAuteur,Titre,Disponible,AnnePublication,ISBN,IdCategorie,IdGenre")] Livre livre)
        {
            var formData = HttpContext.Request.Form;
            foreach (var key in formData.Keys)
            {
                Console.WriteLine($"{key} = {formData[key]}");
            }

            // Also log the raw query string (though it's a POST)
            Console.WriteLine("Raw Form Data: " + HttpContext.Request.GetEncodedUrl());

            if (ModelState.IsValid)
            {
                _context.Add(livre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            foreach (var entry in ModelState)
            {
                foreach (var error in entry.Value.Errors)
                {
                    Console.WriteLine($"ModelState Error on '{entry.Key}': {error.ErrorMessage}");
                }
            }

            // Re-populate ViewData and return view as before
            ViewData["IdAuteur"] = new SelectList(_context.Auteurs, "Id", "NomAuteur", livre.IdAuteur);
            ViewData["IdCategorie"] = new SelectList(_context.Categories, "Id", "NomCategorie", livre.IdCategorie);
            ViewData["IdGenre"] = new SelectList(_context.Genres, "Id", "NomGenre", livre.IdGenre);
            return View(livre);
        }

        // GET: Livres/Edit/5
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
