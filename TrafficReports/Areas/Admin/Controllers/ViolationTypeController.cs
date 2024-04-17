using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Violations;

namespace TrafficReports.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ViolationTypeController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ViolationTypeController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Admin/ViolationType
        public async Task<IActionResult> Index()
        {
            var res = await _uow.ViolationTypes.GetAllAsync();
            return View(res);
        }

        // GET: Admin/ViolationType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violationType = await _uow.ViolationTypes
                .FirstOrDefaultAsync(id.Value);
            if (violationType == null)
            {
                return NotFound();
            }

            return View(violationType);
        }

        // GET: Admin/ViolationType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ViolationType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ViolationTypeName,Severity,Id")] ViolationType violationType)
        {
            if (ModelState.IsValid)
            {
                violationType.Id = Guid.NewGuid();
                _uow.ViolationTypes.Add(violationType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(violationType);
        }

        // GET: Admin/ViolationType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violationType = await _uow.ViolationTypes.FirstOrDefaultAsync(id.Value);
            if (violationType == null)
            {
                return NotFound();
            }
            return View(violationType);
        }

        // POST: Admin/ViolationType/Edit/5
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
                    _uow.ViolationTypes.Update(violationType);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _uow.ViolationTypes.ExistsAsync(violationType.Id))
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

        // GET: Admin/ViolationType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violationType = await _uow.ViolationTypes
                .FirstOrDefaultAsync( id.Value);
            if (violationType == null)
            {
                return NotFound();
            }

            return View(violationType);
        }

        // POST: Admin/ViolationType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _uow.ViolationTypes.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
