using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Violations;

namespace TrafficReports.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ViolationController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ViolationController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Admin/Violation
        public async Task<IActionResult> Index()
        {
            var res = await _uow.Violations.GetAllAsync();
            return View(res);
        }

        // GET: Admin/Violation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violation = await _uow.Violations
                .FirstOrDefaultAsync(id.Value);
            if (violation == null)
            {
                return NotFound();
            }

            return View(violation);
        }

        // GET: Admin/Violation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Violation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ViolationTypeId,ViolationName,Severity,Id")] Violation violation)
        {
            if (ModelState.IsValid)
            {
                violation.Id = Guid.NewGuid();
                _uow.Add(violation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ViolationTypeId"] = new SelectList(_context.ViolationTypes, "Id", "Id", violation.ViolationTypeId);
            return View(violation);
        }

        // GET: Admin/Violation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violation = await _context.Violations.FindAsync(id);
            if (violation == null)
            {
                return NotFound();
            }
            ViewData["ViolationTypeId"] = new SelectList(_context.ViolationTypes, "Id", "Id", violation.ViolationTypeId);
            return View(violation);
        }

        // POST: Admin/Violation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ViolationTypeId,ViolationName,Severity,Id")] Violation violation)
        {
            if (id != violation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(violation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViolationExists(violation.Id))
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
            ViewData["ViolationTypeId"] = new SelectList(_context.ViolationTypes, "Id", "Id", violation.ViolationTypeId);
            return View(violation);
        }

        // GET: Admin/Violation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violation = await _context.Violations
                .Include(v => v.ViolationType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (violation == null)
            {
                return NotFound();
            }

            return View(violation);
        }

        // POST: Admin/Violation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var violation = await _context.Violations.FindAsync(id);
            if (violation != null)
            {
                _context.Violations.Remove(violation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViolationExists(Guid id)
        {
            return _context.Violations.Any(e => e.Id == id);
        }
    }
}
