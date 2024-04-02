using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Violations;

namespace TrafficReports.Controllers
{
    public class ViolationTypeController : Controller
    {
        private readonly AppDbContext _context;

        public ViolationTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ViolationType
        public async Task<IActionResult> Index()
        {
            return View(await _context.ViolationTypes.ToListAsync());
        }

        // GET: ViolationType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violationType = await _context.ViolationTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (violationType == null)
            {
                return NotFound();
            }

            return View(violationType);
        }

        // GET: ViolationType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ViolationType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ViolationTypeName,Severity,Id")] ViolationType violationType)
        {
            if (ModelState.IsValid)
            {
                violationType.Id = Guid.NewGuid();
                _context.Add(violationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(violationType);
        }

        // GET: ViolationType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violationType = await _context.ViolationTypes.FindAsync(id);
            if (violationType == null)
            {
                return NotFound();
            }
            return View(violationType);
        }

        // POST: ViolationType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ViolationTypeName,Severity,Id")] ViolationType violationType)
        {
            if (id != violationType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(violationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViolationTypeExists(violationType.Id))
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
            return View(violationType);
        }

        // GET: ViolationType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violationType = await _context.ViolationTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (violationType == null)
            {
                return NotFound();
            }

            return View(violationType);
        }

        // POST: ViolationType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var violationType = await _context.ViolationTypes.FindAsync(id);
            if (violationType != null)
            {
                _context.ViolationTypes.Remove(violationType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViolationTypeExists(Guid id)
        {
            return _context.ViolationTypes.Any(e => e.Id == id);
        }
    }
}
