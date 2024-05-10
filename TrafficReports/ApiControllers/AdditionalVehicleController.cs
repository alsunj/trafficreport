using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Vehicles;

namespace TrafficReports.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalVehicleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdditionalVehicleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AdditionalVehicle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdditionalVehicle>>> GetAdditionalVehicles()
        {
            return await _context.AdditionalVehicles.ToListAsync();
        }

        // GET: api/AdditionalVehicle/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdditionalVehicle>> GetAdditionalVehicle(Guid id)
        {
            var additionalVehicle = await _context.AdditionalVehicles.FindAsync(id);

            if (additionalVehicle == null)
            {
                return NotFound();
            }

            return additionalVehicle;
        }

        // PUT: api/AdditionalVehicle/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdditionalVehicle(Guid id, AdditionalVehicle additionalVehicle)
        {
            if (id != additionalVehicle.Id)
            {
                return BadRequest();
            }

            _context.Entry(additionalVehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdditionalVehicleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AdditionalVehicle
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdditionalVehicle>> PostAdditionalVehicle(AdditionalVehicle additionalVehicle)
        {
            _context.AdditionalVehicles.Add(additionalVehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdditionalVehicle", new { id = additionalVehicle.Id }, additionalVehicle);
        }

        // DELETE: api/AdditionalVehicle/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdditionalVehicle(Guid id)
        {
            var additionalVehicle = await _context.AdditionalVehicles.FindAsync(id);
            if (additionalVehicle == null)
            {
                return NotFound();
            }

            _context.AdditionalVehicles.Remove(additionalVehicle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdditionalVehicleExists(Guid id)
        {
            return _context.AdditionalVehicles.Any(e => e.Id == id);
        }
    }
}
