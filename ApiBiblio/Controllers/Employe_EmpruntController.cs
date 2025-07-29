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
    public class Employe_EmpruntController : Controller
    {
        private readonly BiblioDb _context;

        public Employe_EmpruntController(BiblioDb context)
        {
            _context = context;
        }

        // GET: Employe_Emprunt
        public async Task<IActionResult> Index()
        {
            var biblioDb = _context.Employe_Emprunt.Include(e => e.Employe).Include(e => e.Emprunt);
            return View(await biblioDb.ToListAsync());
        }

        // GET: Employe_Emprunt/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employe_Emprunt = await _context.Employe_Emprunt
                .Include(e => e.Employe)
                .Include(e => e.Emprunt)
                .FirstOrDefaultAsync(m => m.EmpruntId == id);
            if (employe_Emprunt == null)
            {
                return NotFound();
            }

            return View(employe_Emprunt);
        }

        // GET: Employe_Emprunt/Create
        public IActionResult Create()
        {
            ViewData["EmployeId"] = new SelectList(_context.Employes, "Id", "LoginEmploye");
            ViewData["EmpruntId"] = new SelectList(_context.Emprunts, "Id", "Id");
            return View();
        }

        // POST: Employe_Emprunt/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeId,EmpruntId")] Employe_Emprunt employe_Emprunt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employe_Emprunt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeId"] = new SelectList(_context.Employes, "Id", "LoginEmploye", employe_Emprunt.EmployeId);
            ViewData["EmpruntId"] = new SelectList(_context.Emprunts, "Id", "Id", employe_Emprunt.EmpruntId);
            return View(employe_Emprunt);
        }

        // GET: Employe_Emprunt/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employe_Emprunt = await _context.Employe_Emprunt.FindAsync(id);
            if (employe_Emprunt == null)
            {
                return NotFound();
            }
            ViewData["EmployeId"] = new SelectList(_context.Employes, "Id", "LoginEmploye", employe_Emprunt.EmployeId);
            ViewData["EmpruntId"] = new SelectList(_context.Emprunts, "Id", "Id", employe_Emprunt.EmpruntId);
            return View(employe_Emprunt);
        }

        // POST: Employe_Emprunt/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeId,EmpruntId")] Employe_Emprunt employe_Emprunt)
        {
            if (id != employe_Emprunt.EmpruntId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employe_Emprunt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Employe_EmpruntExists(employe_Emprunt.EmpruntId))
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
            ViewData["EmployeId"] = new SelectList(_context.Employes, "Id", "LoginEmploye", employe_Emprunt.EmployeId);
            ViewData["EmpruntId"] = new SelectList(_context.Emprunts, "Id", "Id", employe_Emprunt.EmpruntId);
            return View(employe_Emprunt);
        }

        // GET: Employe_Emprunt/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employe_Emprunt = await _context.Employe_Emprunt
                .Include(e => e.Employe)
                .Include(e => e.Emprunt)
                .FirstOrDefaultAsync(m => m.EmpruntId == id);
            if (employe_Emprunt == null)
            {
                return NotFound();
            }

            return View(employe_Emprunt);
        }

        // POST: Employe_Emprunt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employe_Emprunt = await _context.Employe_Emprunt.FindAsync(id);
            if (employe_Emprunt != null)
            {
                _context.Employe_Emprunt.Remove(employe_Emprunt);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Employe_EmpruntExists(int id)
        {
            return _context.Employe_Emprunt.Any(e => e.EmpruntId == id);
        }
    }
}
