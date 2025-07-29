using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiBiblio.Database;
using ApiBiblio.Models;
using Microsoft.AspNetCore.Identity; // AJOUT : Pour le hashage du mot de passe

namespace ApiBiblio.Controllers
{
    public class EmployesController : Controller
    {
        private readonly BiblioDb _context;

        public EmployesController(BiblioDb context)
        {
            _context = context;
        }

        // GET: Employes
        public async Task<IActionResult> Index()
        {
            var biblioDb = _context.Employes
                .Include(e => e.Role)
                .AsNoTracking();
            return View(await biblioDb.ToListAsync());
        }

        // GET: Employes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employe = await _context.Employes
                .Include(e => e.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employe == null)
            {
                return NotFound();
            }

            return View(employe);
        }

        // GET: Employes/Create
        public IActionResult Create()
        {
            ViewData["IdRole"] = new SelectList(_context.Roles, "Id", "NomRole");
            return View();
        }

        // POST: Employes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomEmploye,PrenomEmploye,LoginEmploye,MdpEmploye,IdRole")] Employe employe)
        {
            if (ModelState.IsValid)
            {
                // AJOUT : Hashage du mot de passe avant sauvegarde
                var hasher = new PasswordHasher<Employe>();
                employe.MdpEmploye = hasher.HashPassword(employe, employe.MdpEmploye);

                _context.Add(employe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRole"] = new SelectList(_context.Roles, "Id", "NomRole", employe.IdRole);
            return View(employe);
        }

        // GET: Employes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employe = await _context.Employes.FindAsync(id);
            if (employe == null)
            {
                return NotFound();
            }
            ViewData["IdRole"] = new SelectList(_context.Roles, "Id", "NomRole", employe.IdRole);
            return View(employe);
        }

        // POST: Employes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomEmploye,PrenomEmploye,LoginEmploye,MdpEmploye,IdRole")] Employe employe)
        {
            if (id != employe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // AJOUT : Hashage du mot de passe avant sauvegarde
                    var hasher = new PasswordHasher<Employe>();
                    employe.MdpEmploye = hasher.HashPassword(employe, employe.MdpEmploye);

                    _context.Update(employe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeExists(employe.Id))
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
            ViewData["IdRole"] = new SelectList(_context.Roles, "Id", "NomRole", employe.IdRole);
            return View(employe);
        }

        // GET: Employes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employe = await _context.Employes
                .Include(e => e.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employe == null)
            {
                return NotFound();
            }

            return View(employe);
        }

        // POST: Employes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employe = await _context.Employes.FindAsync(id);
            if (employe != null)
            {
                _context.Employes.Remove(employe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeExists(int id)
        {
            return _context.Employes.Any(e => e.Id == id);
        }
    }
}
