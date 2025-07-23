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
    public class Livre_AuteurController : Controller
    {
        private readonly BiblioDb _context;

        public Livre_AuteurController(BiblioDb context)
        {
            _context = context;
        }

        // GET: Livre_Auteur
        public async Task<IActionResult> Index()
        {
            return View(await _context.Livre_Auteur.ToListAsync());
        }

        // GET: Livre_Auteur/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre_Auteur = await _context.Livre_Auteur
                .FirstOrDefaultAsync(m => m.IdLivre == id);
            if (livre_Auteur == null)
            {
                return NotFound();
            }

            return View(livre_Auteur);
        }

        // GET: Livre_Auteur/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Livre_Auteur/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLivre,IdAuteur")] Livre_Auteur livre_Auteur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(livre_Auteur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(livre_Auteur);
        }

        // GET: Livre_Auteur/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre_Auteur = await _context.Livre_Auteur.FindAsync(id);
            if (livre_Auteur == null)
            {
                return NotFound();
            }
            return View(livre_Auteur);
        }

        // POST: Livre_Auteur/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLivre,IdAuteur")] Livre_Auteur livre_Auteur)
        {
            if (id != livre_Auteur.IdLivre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livre_Auteur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Livre_AuteurExists(livre_Auteur.IdLivre))
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
            return View(livre_Auteur);
        }

        // GET: Livre_Auteur/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre_Auteur = await _context.Livre_Auteur
                .FirstOrDefaultAsync(m => m.IdLivre == id);
            if (livre_Auteur == null)
            {
                return NotFound();
            }

            return View(livre_Auteur);
        }

        // POST: Livre_Auteur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livre_Auteur = await _context.Livre_Auteur.FindAsync(id);
            if (livre_Auteur != null)
            {
                _context.Livre_Auteur.Remove(livre_Auteur);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Livre_AuteurExists(int id)
        {
            return _context.Livre_Auteur.Any(e => e.IdLivre == id);
        }
    }
}
