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
/*
namespace WebApp.Areas.Admin.Controllers
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
            var res = await _uow.ViolationRepository.GetAllWithViolationTypesAsync();
            return View(res);
        }

        // GET: Admin/Violation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violation = await _uow.ViolationRepository
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
            ViewData["ViolationTypeId"] = new SelectList(_uow.ViolationTypeRepository.GetAll(), "Id", "Id");
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
                _uow.ViolationRepository.Add(violation);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ViolationTypeId"] = new SelectList(await _uow.ViolationTypeRepository.GetAllAsync(), "Id", "Id", violation.ViolationTypeId);
            return View(violation);
        }

        // GET: Admin/Violation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violation = await _uow.ViolationRepository.FirstOrDefaultAsync(id.Value);
            if (violation == null)
            {
                return NotFound();
            }
            ViewData["ViolationTypeId"] = new SelectList(await _uow.ViolationTypeRepository.GetAllAsync(), "Id", "Id", violation.ViolationTypeId);
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
                    _uow.ViolationRepository.Update(violation);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _uow.ViolationRepository.ExistsAsync(violation.Id))
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
            ViewData["ViolationTypeId"] = new SelectList(await _uow.ViolationTypeRepository.GetAllAsync(), "Id", "Id", violation.ViolationTypeId);
            return View(violation);
        }

        // GET: Admin/Violation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violation = await _uow.ViolationRepository
                //    .Include(v => v.ViolationType)
                //     .FirstOrDefaultAsync(m => m.Id == id);
                .FirstOrDefaultAsync(id.Value);
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
            _uow.ViolationRepository.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
*/
