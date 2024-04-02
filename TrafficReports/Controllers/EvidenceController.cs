using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Evidences;

namespace TrafficReports.Controllers
{
    public class EvidenceController : Controller
    {
        private readonly AppDbContext _context;

        public EvidenceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Evidence
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Evidences.Include(e => e.EvidenceType).Include(e => e.VehicleViolation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Evidence/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evidence = await _context.Evidences
                .Include(e => e.EvidenceType)
                .Include(e => e.VehicleViolation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evidence == null)
            {
                return NotFound();
            }

            return View(evidence);
        }

        // GET: Evidence/Create
        public IActionResult Create()
        {
            ViewData["EvidenceTypeId"] = new SelectList(_context.EvidenceTypes, "Id", "Id");
            ViewData["VehicleViolationId"] = new SelectList(_context.VehicleViolations, "Id", "Id");
            return View();
        }

        // POST: Evidence/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EvidenceTypeId,VehicleViolationId,Description,Id")] Evidence evidence)
        {
            if (ModelState.IsValid)
            {
                evidence.Id = Guid.NewGuid();
                _context.Add(evidence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EvidenceTypeId"] = new SelectList(_context.EvidenceTypes, "Id", "Id", evidence.EvidenceTypeId);
            ViewData["VehicleViolationId"] = new SelectList(_context.VehicleViolations, "Id", "Id", evidence.VehicleViolationId);
            return View(evidence);
        }

        // GET: Evidence/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evidence = await _context.Evidences.FindAsync(id);
            if (evidence == null)
            {
                return NotFound();
            }
            ViewData["EvidenceTypeId"] = new SelectList(_context.EvidenceTypes, "Id", "Id", evidence.EvidenceTypeId);
            ViewData["VehicleViolationId"] = new SelectList(_context.VehicleViolations, "Id", "Id", evidence.VehicleViolationId);
            return View(evidence);
        }

        // POST: Evidence/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EvidenceTypeId,VehicleViolationId,Description,Id")] Evidence evidence)
        {
            if (id != evidence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evidence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvidenceExists(evidence.Id))
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
            ViewData["EvidenceTypeId"] = new SelectList(_context.EvidenceTypes, "Id", "Id", evidence.EvidenceTypeId);
            ViewData["VehicleViolationId"] = new SelectList(_context.VehicleViolations, "Id", "Id", evidence.VehicleViolationId);
            return View(evidence);
        }

        // GET: Evidence/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evidence = await _context.Evidences
                .Include(e => e.EvidenceType)
                .Include(e => e.VehicleViolation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evidence == null)
            {
                return NotFound();
            }

            return View(evidence);
        }

        // POST: Evidence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var evidence = await _context.Evidences.FindAsync(id);
            if (evidence != null)
            {
                _context.Evidences.Remove(evidence);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvidenceExists(Guid id)
        {
            return _context.Evidences.Any(e => e.Id == id);
        }
    }
}
