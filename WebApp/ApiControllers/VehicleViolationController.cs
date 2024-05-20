
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
    [ApiController]
    [Route("api/v{version:apiVersion}/violations/[controller]/[action]")]



    public class VehicleViolationController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.VehicleViolation, App.BLL.DTO.VehicleViolation> _mapper;

        public VehicleViolationController(IAppBLL bll, UserManager<AppUser> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.VehicleViolation, App.BLL.DTO.VehicleViolation>(autoMapper);
            
        }


        // GET: api/VehicleViolation
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.DTO.v1_0.VehicleViolation>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.VehicleViolation>>> GetVehicleViolations()
        {
            var bllVehicleViolationResult = await _bll.VehicleViolations.GetAllAsync();
            var bllVehiceViolation =  bllVehicleViolationResult.Select(e => _mapper.Map(e)).ToList();
            return Ok(bllVehiceViolation);
        }

        // GET: api/VehicleViolation/5
        [HttpGet("{id}")]
        [ProducesResponseType<IEnumerable<App.DTO.v1_0.VehicleViolation>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.VehicleViolation>> GetVehicleViolation(Guid id)
        {
            var vehicleViolation = await _bll.VehicleViolations.FirstOrDefaultAsync(id);

            if (vehicleViolation == null)
            {
                return NotFound();
            }

            var res = _mapper.Map(vehicleViolation);
            return Ok(res);
        }

        // PUT: api/VehicleViolation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutVehicleViolation(Guid id, App.DTO.v1_0.VehicleViolation vehicleViolation)
        {
            if (id != vehicleViolation.Id)
            {
                return BadRequest("bad request");
            }

            if (!await _bll.VehicleViolations.ExistsAsync(id))
            {
                return NotFound("id doesnt exist");
            }

            var res = _mapper.Map(vehicleViolation);
            
            _bll.VehicleViolations.Update(res);
            await _bll.SaveChangesAsync();
            
            return NoContent();

        }

        // POST: api/VehicleViolation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<IEnumerable<App.DTO.v1_0.VehicleViolation>>((int) HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.VehicleViolation>> PostVehicleViolation(App.DTO.v1_0.VehicleViolation vehicleViolation)
        {
            var mappedVehicleViolation = _mapper.Map(vehicleViolation);
            _bll.VehicleViolations.Add(mappedVehicleViolation);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetVehicleViolation", new
            {
                version = HttpContext.GetRequestedApiVersion()?.ToString(),
                id = mappedVehicleViolation.Id
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
            var vehicleViolation = await _bll.VehicleViolations.ExistsAsync(id);
            if (vehicleViolation == null)
            {
                return NotFound();
            }

            await _bll.VehicleViolations.RemoveAsync(id);

            
            return NoContent();
        }

        private bool VehicleViolationExists(Guid id)
        {
            return _bll.VehicleViolations.Exists(id);
        }
    }

}
