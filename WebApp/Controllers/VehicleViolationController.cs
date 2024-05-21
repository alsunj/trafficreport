using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.DAL.EF.Repositories;
using App.Domain;
using App.Domain.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TrafficReport.Helpers;


namespace TrafficReport.Controllers
{
    [Authorize]
    public class VehicleViolationController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.VehicleViolation, App.BLL.DTO.VehicleViolation> _mapper;

        public VehicleViolationController(IAppUnitOfWork uow, UserManager<AppUser> userManager, IMapper autoMapper)
        {
            _uow = uow;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.VehicleViolation, App.BLL.DTO.VehicleViolation>(autoMapper);
        }
        
        
        

        // GET: /VehicleViolations
        public async Task<IActionResult> Index()
        {
            var res = await _uow.VehicleViolationRepository.GetAllSortedAsync(
                Guid.Parse(_userManager.GetUserId(User)));
            return View(res);
        }

        // GET: Admin/VehicleViolations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vehicleViolation = await _uow.VehicleViolationRepository.FirstOrDefaultAsync(id.Value);
            
            if (vehicleViolation == null)
            {
                return NotFound();
            }

            return View(vehicleViolation);
        }

        // GET: Admin/VehicleViolations/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_uow.AppUserRepository.GetAll(), "Id", "Id");
            ViewData["VehicleId"] = new SelectList(_uow.VehicleRepository.GetAll(), "Id", "Id");
            ViewData["ViolationId"] = new SelectList(_uow.ViolationRepository.GetAll(), "Id", "Id");
            return View();
        }

        // POST: Admin/VehicleViolations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(App.DAL.DTO.VehicleViolation vehicleViolation)
        {
            if (ModelState.IsValid)
            {
                _uow.VehicleViolationRepository.Add(vehicleViolation);
                
                var vehicle = await _uow.VehicleRepository.FirstOrDefaultAsync(vehicleViolation.VehicleId);
                
                vehicle!.Rating = (decimal) _uow.VehicleRepository.CalculateVehicleRatingByLicensePlate(vehicle.RegNr!);
                
                _uow.VehicleRepository.Update(vehicle);
                
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(await _uow.AppUserRepository.GetAllAsync(), "Id", "Id", vehicleViolation.AppUserId);
            ViewData["VehicleId"] = new SelectList(await _uow.VehicleRepository.GetAllAsync(), "Id", "Id", vehicleViolation.VehicleId);
            ViewData["ViolationId"] = new SelectList(await _uow.ViolationRepository.GetAllAsync(), "Id", "Id", vehicleViolation.ViolationId);
            return View(vehicleViolation);
        }

        // GET: Admin/VehicleViolations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleViolation = await _uow.VehicleViolationRepository.FirstOrDefaultAsync(id.Value);
            if (vehicleViolation == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(await _uow.AppUserRepository.GetAllAsync(), "Id", "Id", vehicleViolation.AppUserId);
            ViewData["VehicleId"] = new SelectList(await _uow.VehicleRepository.GetAllAsync(), "Id", "Id", vehicleViolation.VehicleId);
            ViewData["ViolationId"] = new SelectList(await _uow.ViolationRepository.GetAllAsync(), "Id", "Id", vehicleViolation.ViolationId);
            return View(vehicleViolation);
        }

        // POST: Admin/VehicleViolations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("VehicleId,ViolationId,AppUserId,Description,Coordinates,LocationName,CreatedAt,Id")] App.DAL.DTO.VehicleViolation vehicleViolation)
        {
            if (id != vehicleViolation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.VehicleViolationRepository.Update(vehicleViolation);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.VehicleViolationRepository.ExistsAsync(vehicleViolation.Id))
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
            ViewData["AppUserId"] = new SelectList(await _uow.AppUserRepository.GetAllAsync(), "Id", "Id", vehicleViolation.AppUserId);
            ViewData["VehicleId"] = new SelectList(await _uow.VehicleRepository.GetAllAsync(), "Id", "Id", vehicleViolation.VehicleId);
            ViewData["ViolationId"] = new SelectList(await _uow.ViolationRepository.GetAllAsync(), "Id", "Id", vehicleViolation.ViolationId);
            return View(vehicleViolation);
        }

        // GET: Admin/VehicleViolations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleViolation = await _uow.VehicleViolationRepository
                // .Include(v => v.AppUser)
                // .Include(v => v.Vehicle)
                // .Include(v => v.Violation)
                .FirstOrDefaultAsync(id.Value);
            if (vehicleViolation == null)
            {
                return NotFound();
            }

            return View(vehicleViolation);
        }

        // POST: Admin/VehicleViolations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.VehicleViolationRepository.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}