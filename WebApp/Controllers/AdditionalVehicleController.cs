using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Vehicles;

namespace TrafficReports.Controllers
{
    public class AdditionalVehicleController : Controller
    {
        private readonly AppDbContext _context;

        public AdditionalVehicleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AdditionalVehicle
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AdditionalVehicles.Include(a => a.Vehicle).Include(a => a.VehicleViolation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AdditionalVehicle/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalVehicle = await _context.AdditionalVehicles
                .Include(a => a.Vehicle)
                .Include(a => a.VehicleViolation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (additionalVehicle == null)
            {
                return NotFound();
            }

            return View(additionalVehicle);
        }

        // GET: AdditionalVehicle/Create
        public IActionResult Create()
        {
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id");
            ViewData["VehicleViolationId"] = new SelectList(_context.VehicleViolations, "Id", "Id");
            return View();
        }

        // POST: AdditionalVehicle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleId,VehicleViolationId,Id")] AdditionalVehicle additionalVehicle)
        {
            if (ModelState.IsValid)
            {
                additionalVehicle.Id = Guid.NewGuid();
                _context.Add(additionalVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id", additionalVehicle.VehicleId);
            ViewData["VehicleViolationId"] = new SelectList(_context.VehicleViolations, "Id", "Id", additionalVehicle.VehicleViolationId);
            return View(additionalVehicle);
        }

        // GET: AdditionalVehicle/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalVehicle = await _context.AdditionalVehicles.FindAsync(id);
            if (additionalVehicle == null)
            {
                return NotFound();
            }
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id", additionalVehicle.VehicleId);
            ViewData["VehicleViolationId"] = new SelectList(_context.VehicleViolations, "Id", "Id", additionalVehicle.VehicleViolationId);
            return View(additionalVehicle);
        }

        // POST: AdditionalVehicle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("VehicleId,VehicleViolationId,Id")] AdditionalVehicle additionalVehicle)
        {
            if (id != additionalVehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(additionalVehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdditionalVehicleExists(additionalVehicle.Id))
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
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id", additionalVehicle.VehicleId);
            ViewData["VehicleViolationId"] = new SelectList(_context.VehicleViolations, "Id", "Id", additionalVehicle.VehicleViolationId);
            return View(additionalVehicle);
        }

        // GET: AdditionalVehicle/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalVehicle = await _context.AdditionalVehicles
                .Include(a => a.Vehicle)
                .Include(a => a.VehicleViolation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (additionalVehicle == null)
            {
                return NotFound();
            }

            return View(additionalVehicle);
        }

        // POST: AdditionalVehicle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var additionalVehicle = await _context.AdditionalVehicles.FindAsync(id);
            if (additionalVehicle != null)
            {
                _context.AdditionalVehicles.Remove(additionalVehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdditionalVehicleExists(Guid id)
        {
            return _context.AdditionalVehicles.Any(e => e.Id == id);
        }
    }
}
