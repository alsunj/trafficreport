using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.DTO.v1_0;
using TrafficReport.ViewModels;
using Evidence = App.Domain.Evidences.Evidence;

namespace WebApp.Controllers
{
    public class EvidenceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EvidenceController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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
            ViewData["VehicleViolationId"] = new SelectList(_context.VehicleViolations, "Id", "LocationName");
            return View();
        }

        // // POST: Evidence/Create
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("EvidenceTypeId,VehicleViolationId,Files,Description,Id")] Evidence evidence)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         evidence.Id = Guid.NewGuid();
        //         _context.Add(evidence);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     ViewData["EvidenceTypeId"] = new SelectList(_context.EvidenceTypes, "Id", "Id", evidence.EvidenceTypeId);
        //     ViewData["VehicleViolationId"] = new SelectList(_context.VehicleViolations, "Id", "LocationName", evidence.VehicleViolationId);
        //     return View(evidence);
        // }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EvidenceTypeId,VehicleViolationId,File,Description")] Evidence evidence, IFormFile file)
        {
            
            var fileExtensions = new string[]
            {
                ".png", ".jpg", ".bmp", ".gif", ".mov", ".mp4"
            };
            
            if (ModelState.IsValid)
            {
                evidence.Id = Guid.NewGuid();
                _context.Add(evidence);
                await _context.SaveChangesAsync();
                if (file.Length > 0 && fileExtensions.Contains(Path.GetExtension(file.FileName)))
                {
                    var uploadDir = Path.Combine(_env.WebRootPath, "uploads");
                    
                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }
                    
                    var filename = evidence.Id + "_" + Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(uploadDir, filename);

                    await using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                    
                    evidence.File = "~/uploads/" + filename;
                    _context.Update(evidence);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.EvidenceTypeId = new SelectList(_context.EvidenceTypes, "Id", "Name", evidence.EvidenceTypeId);
            ViewBag.VehicleViolationId = new SelectList(_context.VehicleViolations, "Id", "Name", evidence.VehicleViolationId);
            return RedirectToAction(nameof(Index));
        }
        
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
            ViewData["VehicleViolationId"] = new SelectList(_context.VehicleViolations, "Id", "VehicleViolationId", evidence.VehicleViolationId);
            return View(evidence);
        }

        // POST: Evidence/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EvidenceTypeId,VehicleViolationId,File,Description,Id")] Evidence evidence)
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
            ViewData["VehicleViolationId"] = new SelectList(_context.VehicleViolations, "Id", "VehicleViolationId", evidence.VehicleViolationId);
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
