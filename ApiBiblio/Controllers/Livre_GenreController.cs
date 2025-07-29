using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApiBiblio.Database;
using ApiBiblio.Models;

namespace ApiBiblio.Controllers
{
    public class Livre_GenreController : Controller
    {
        private readonly BiblioDb _context;

        public Livre_GenreController(BiblioDb context)
        {
            _context = context;
        }

        // GET: Livre_Genre
        public async Task<IActionResult> Index()
        {
            var biblioDb = _context.Livre_Genre.Include(l => l.Genre).Include(l => l.Livre);
            return View(await biblioDb.ToListAsync());
        }

        // GET: Livre_Genre/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre_Genre = await _context.Livre_Genre
                .Include(l => l.Genre)
                .Include(l => l.Livre)
                .FirstOrDefaultAsync(m => m.IdLivre == id);
            if (livre_Genre == null)
            {
                return NotFound();
            }

            return View(livre_Genre);
        }

        // GET: Livre_Genre/Create
        public IActionResult Create()
        {
            ViewData["IdGenre"] = new SelectList(_context.Genres, "Id", "NomGenre");
            ViewData["IdLivre"] = new SelectList(_context.Livres, "Id", "Titre");
            return View();
        }

        // POST: Livre_Genre/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLivre,IdGenre")] Livre_Genre livre_Genre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(livre_Genre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdGenre"] = new SelectList(_context.Genres, "Id", "NomGenre", livre_Genre.IdGenre);
            ViewData["IdLivre"] = new SelectList(_context.Livres, "Id", "Titre", livre_Genre.IdLivre);
            return View(livre_Genre);
        }

        // GET: Livre_Genre/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre_Genre = await _context.Livre_Genre.FindAsync(id);
            if (livre_Genre == null)
            {
                return NotFound();
            }
            ViewData["IdGenre"] = new SelectList(_context.Genres, "Id", "NomGenre", livre_Genre.IdGenre);
            ViewData["IdLivre"] = new SelectList(_context.Livres, "Id", "Titre", livre_Genre.IdLivre);
            return View(livre_Genre);
        }

        // POST: Livre_Genre/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLivre,IdGenre")] Livre_Genre livre_Genre)
        {
            if (id != livre_Genre.IdLivre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livre_Genre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Livre_GenreExists(livre_Genre.IdLivre))
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
            ViewData["IdGenre"] = new SelectList(_context.Genres, "Id", "NomGenre", livre_Genre.IdGenre);
            ViewData["IdLivre"] = new SelectList(_context.Livres, "Id", "Titre", livre_Genre.IdLivre);
            return View(livre_Genre);
        }

        // GET: Livre_Genre/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre_Genre = await _context.Livre_Genre
                .Include(l => l.Genre)
                .Include(l => l.Livre)
                .FirstOrDefaultAsync(m => m.IdLivre == id);
            if (livre_Genre == null)
            {
                return NotFound();
            }

            return View(livre_Genre);
        }

        // POST: Livre_Genre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livre_Genre = await _context.Livre_Genre.FindAsync(id);
            if (livre_Genre != null)
            {
                _context.Livre_Genre.Remove(livre_Genre);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Livre_GenreExists(int id)
        {
            return _context.Livre_Genre.Any(e => e.IdLivre == id);
        }
    }
}
