
using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Identity;
using App.Domain.Violations;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TrafficReport.Helpers;

namespace TrafficReports.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiVersion("0.9", Deprecated = true)]
    [ApiController]
    [Route("api/v{version:ApiVersion}/violations/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


    public class VehicleViolationController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.VehicleViolation, App.BLL.DTO.VehicleViolation> _mapper;

        public VehicleViolationController(AppDbContext context, IAppBLL bll, UserManager<AppUser> userManager,
            IMapper autoMapper, PublicDTOBllMapper<App.DTO.v1_0.VehicleViolation, App.BLL.DTO.VehicleViolation> mapper)
        {
            _context = context;
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.VehicleViolation, App.BLL.DTO.VehicleViolation>(autoMapper);        }


        // GET: api/VehicleViolation
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.VehicleViolation>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<VehicleViolation>>> GetVehicleViolations()
        {
            return await _context.VehicleViolations.ToListAsync();
        }

        // GET: api/VehicleViolation/5
        [HttpGet("{id}")]
        [ProducesResponseType<VehicleViolation>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<VehicleViolation>> GetVehicleViolation(Guid id)
        {
            var vehicleViolation = await _context.VehicleViolations.FindAsync(id);

            if (vehicleViolation == null)
            {
                return NotFound();
            }

            return vehicleViolation;
        }

        // PUT: api/VehicleViolation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutVehicleViolation(Guid id, VehicleViolation vehicleViolation)
        {
            if (id != vehicleViolation.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicleViolation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleViolationExists(id))
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

        // POST: api/VehicleViolation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<VehicleViolation>((int) HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<VehicleViolation>> PostVehicleViolation(VehicleViolation vehicleViolation)
        {
            _context.VehicleViolations.Add(vehicleViolation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleViolation", new
            {
                version = HttpContext.GetRequestedApiVersion()?.ToString(),
                id = vehicleViolation.Id
            }, vehicleViolation);
        }

        // DELETE: api/VehicleViolation/5
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        
        public async Task<IActionResult> DeleteVehicleViolation(Guid id)
        {
            var vehicleViolation = await _context.VehicleViolations.FindAsync(id);
            if (vehicleViolation == null)
            {
                return NotFound();
            }

            _context.VehicleViolations.Remove(vehicleViolation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleViolationExists(Guid id)
        {
            return _context.VehicleViolations.Any(e => e.Id == id);
        }
    }
}
