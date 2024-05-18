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
    public class VehicleViolationController : Controller
    {
        private readonly AppDbContext _context;

        public VehicleViolationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: VehicleViolation
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.VehicleViolations.Include(v => v.Account).Include(v => v.Vehicle).Include(v => v.Violation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: VehicleViolation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleViolation = await _context.VehicleViolations
                .Include(v => v.Account)
                .Include(v => v.Vehicle)
                .Include(v => v.Violation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleViolation == null)
            {
                return NotFound();
            }

            return View(vehicleViolation);
        }

        // GET: VehicleViolation/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id");
            ViewData["ViolationId"] = new SelectList(_context.Violations, "Id", "Id");
            return View();
        }

        // POST: VehicleViolation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleId,ViolationId,AccountId,Description,Coordinates,LocationName,CreatedAt,Id")] VehicleViolation vehicleViolation)
        {
            if (ModelState.IsValid)
            {
                vehicleViolation.Id = Guid.NewGuid();
                _context.Add(vehicleViolation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Users, "Id", "Id", vehicleViolation.AccountId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id", vehicleViolation.VehicleId);
            ViewData["ViolationId"] = new SelectList(_context.Violations, "Id", "Id", vehicleViolation.ViolationId);
            return View(vehicleViolation);
        }

        // GET: VehicleViolation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleViolation = await _context.VehicleViolations.FindAsync(id);
            if (vehicleViolation == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Users, "Id", "Id", vehicleViolation.AccountId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id", vehicleViolation.VehicleId);
            ViewData["ViolationId"] = new SelectList(_context.Violations, "Id", "Id", vehicleViolation.ViolationId);
            return View(vehicleViolation);
        }

        // POST: VehicleViolation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("VehicleId,ViolationId,AccountId,Description,Coordinates,LocationName,CreatedAt,Id")] VehicleViolation vehicleViolation)
        {
            if (id != vehicleViolation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleViolation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleViolationExists(vehicleViolation.Id))
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
            ViewData["AccountId"] = new SelectList(_context.Users, "Id", "Id", vehicleViolation.AccountId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id", vehicleViolation.VehicleId);
            ViewData["ViolationId"] = new SelectList(_context.Violations, "Id", "Id", vehicleViolation.ViolationId);
            return View(vehicleViolation);
        }

        // GET: VehicleViolation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleViolation = await _context.VehicleViolations
                .Include(v => v.Account)
                .Include(v => v.Vehicle)
                .Include(v => v.Violation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleViolation == null)
            {
                return NotFound();
            }

            return View(vehicleViolation);
        }

        // POST: VehicleViolation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var vehicleViolation = await _context.VehicleViolations.FindAsync(id);
            if (vehicleViolation != null)
            {
                _context.VehicleViolations.Remove(vehicleViolation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleViolationExists(Guid id)
        {
            return _context.VehicleViolations.Any(e => e.Id == id);
        }
    }
}
