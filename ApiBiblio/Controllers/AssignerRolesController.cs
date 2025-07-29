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
    public class AssignerRolesController : Controller
    {
        private readonly BiblioDb _context;

        public AssignerRolesController(BiblioDb context)
        {
            _context = context;
        }

        // GET: AssignerRoles
        public async Task<IActionResult> Index()
        {
            var biblioDb = _context.AssignerRole
                .Include(a => a.Employe)
                .Include(a => a.Role)
                .AsNoTracking();
            return View(await biblioDb.ToListAsync());
        }

        // GET: AssignerRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignerRole = await _context.AssignerRole
                .Include(a => a.Employe)
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (assignerRole == null)
            {
                return NotFound();
            }

            return View(assignerRole);
        }

        // GET: AssignerRoles/Create
        public IActionResult Create()
        {
            ViewData["EmployeId"] = new SelectList(_context.Employes, "Id", "LoginEmploye");
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id");
            return View();
        }

        // POST: AssignerRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,EmployeId")] AssignerRole assignerRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assignerRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeId"] = new SelectList(_context.Employes, "Id", "LoginEmploye", assignerRole.EmployeId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", assignerRole.RoleId);
            return View(assignerRole);
        }

        // GET: AssignerRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignerRole = await _context.AssignerRole.FindAsync(id);
            if (assignerRole == null)
            {
                return NotFound();
            }
            ViewData["EmployeId"] = new SelectList(_context.Employes, "Id", "LoginEmploye", assignerRole.EmployeId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", assignerRole.RoleId);
            return View(assignerRole);
        }

        // POST: AssignerRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleId,EmployeId")] AssignerRole assignerRole)
        {
            if (id != assignerRole.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignerRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignerRoleExists(assignerRole.RoleId))
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
            ViewData["EmployeId"] = new SelectList(_context.Employes, "Id", "LoginEmploye", assignerRole.EmployeId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", assignerRole.RoleId);
            return View(assignerRole);
        }

        // GET: AssignerRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignerRole = await _context.AssignerRole
                .Include(a => a.Employe)
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (assignerRole == null)
            {
                return NotFound();
            }

            return View(assignerRole);
        }

        // POST: AssignerRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignerRole = await _context.AssignerRole.FindAsync(id);
            if (assignerRole != null)
            {
                _context.AssignerRole.Remove(assignerRole);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignerRoleExists(int id)
        {
            return _context.AssignerRole.Any(e => e.RoleId == id);
        }
    }
}
