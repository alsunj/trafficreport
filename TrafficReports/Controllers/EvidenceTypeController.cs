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
    public class EvidenceTypeController : Controller
    {
        private readonly AppDbContext _context;

        public EvidenceTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EvidenceType
        public async Task<IActionResult> Index()
        {
            return View(await _context.EvidenceTypes.ToListAsync());
        }

        // GET: EvidenceType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evidenceType = await _context.EvidenceTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evidenceType == null)
            {
                return NotFound();
            }

            return View(evidenceType);
        }

        // GET: EvidenceType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EvidenceType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EvidenceTypeName,Id")] EvidenceType evidenceType)
        {
            if (ModelState.IsValid)
            {
                evidenceType.Id = Guid.NewGuid();
                _context.Add(evidenceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evidenceType);
        }

        // GET: EvidenceType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evidenceType = await _context.EvidenceTypes.FindAsync(id);
            if (evidenceType == null)
            {
                return NotFound();
            }
            return View(evidenceType);
        }

        // POST: EvidenceType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EvidenceTypeName,Id")] EvidenceType evidenceType)
        {
            if (id != evidenceType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evidenceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvidenceTypeExists(evidenceType.Id))
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
            return View(evidenceType);
        }

        // GET: EvidenceType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evidenceType = await _context.EvidenceTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evidenceType == null)
            {
                return NotFound();
            }

            return View(evidenceType);
        }

        // POST: EvidenceType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var evidenceType = await _context.EvidenceTypes.FindAsync(id);
            if (evidenceType != null)
            {
                _context.EvidenceTypes.Remove(evidenceType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvidenceTypeExists(Guid id)
        {
            return _context.EvidenceTypes.Any(e => e.Id == id);
        }
    }
}
