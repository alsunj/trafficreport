
using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using App.Domain.Identity;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TrafficReport.Helpers;

namespace TrafficReport.ApiControllers

{

    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

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


        /// <summary>
        /// Get a list of all vehicle violations.
        /// </summary>
        /// <returns>List of vehicle violations</returns>
      
        [HttpGet("GetVehicleViolations")]
        [ProducesResponseType<List<App.DTO.v1_0.VehicleViolation>>((int) HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.VehicleViolation>>> GetVehicleViolations()
        {
            var bllVehicleViolationResult = await _bll.VehicleViolations.GetAllAsync();
            var bllVehiceViolation =  bllVehicleViolationResult.Select(e => _mapper.Map(e)).ToList();
            return Ok(bllVehiceViolation);
        }
        
        /// <summary>
        /// Get a list of current user's vehicle violations.
        /// </summary>
        /// <returns>List of vehicle violations</returns>
        [HttpGet("GetVehicleViolationsForUser")]
        [ProducesResponseType<List<App.DTO.v1_0.VehicleViolation>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.VehicleViolation>>> GetVehicleViolationsForUser()
        {
            var bllVehicleViolationResult = await _bll.VehicleViolations.
                GetAllUserVehicleViolationsSortedAsync(
                    Guid.Parse(_userManager.GetUserId(User)));
            
            var bllVehiceViolation =  bllVehicleViolationResult.Select(e => _mapper.Map(e)).ToList();
            return Ok(bllVehiceViolation);
        }

        /// <summary>
        /// Get vehicle violation by VehicleViolationId.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>vehicle violation</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<App.DTO.v1_0.VehicleViolation>((int) HttpStatusCode.OK)]
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
        
        /// <summary>
        /// Get vehicle violations by license plate.
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <returns>list of vehicle violations.</returns>
        [HttpGet("GetVehicleViolationsByLicensePlate/{licensePlate}")]
        [ProducesResponseType<List<App.DTO.v1_0.VehicleViolation>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.VehicleViolation>>> GetVehicleViolationsByLicensePlate(string licensePlate)
        {
            var vehicleViolations =
                await _bll.VehicleViolations.GetAllVehicleViolationsByLicensePlateSortedAsync(licensePlate);
                

            if (vehicleViolations.IsNullOrEmpty())
            {
                return NotFound();
            }
            vehicleViolations =  vehicleViolations.ToList();
            
            return Ok(vehicleViolations);
        }
        
        /// <summary>
        /// Get vehicle violations by vehicle id.
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns>list of vehicle violations.</returns>
        [HttpGet("GetAllVehicleViolationsByVehicleId/{vehicleId}")]
        [ProducesResponseType<List<App.DTO.v1_0.VehicleViolation>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.VehicleViolation>>> GetAllVehicleViolationsByVehicleId(Guid vehicleId)
        {
            var vehicleViolations =
                await _bll.VehicleViolations.GetAllVehicleViolationsByVehicleId(vehicleId);
                

            if (vehicleViolations.IsNullOrEmpty())
            {
                return NotFound();
            }
            vehicleViolations =  vehicleViolations.ToList();
            
            return Ok(vehicleViolations);
        }

        /// <summary>
        /// Edit vehicle violation.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vehicleViolation"></param>
        /// <returns></returns>
        [HttpPut("put/{id}")]
        //[ProducesResponseType((int) HttpStatusCode.NoContent)]
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

        /// <summary>
        /// Add vehicle violation.
        /// </summary>
        /// <param name="vehicleViolation"></param>
        /// <returns></returns>
        [HttpPost("post")]
        [ProducesResponseType<App.DTO.v1_0.VehicleViolation>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.VehicleViolation>> PostVehicleViolation(App.DTO.v1_0.VehicleViolation vehicleViolation)
        {
            
            vehicleViolation.Id = Guid.NewGuid();

          //vehicleViolation.AppUserId = Guid.Parse(_userManager.GetUserId(User)!);
            
            var mappedVehicleViolation = _mapper.Map(vehicleViolation);
            _bll.VehicleViolations.Add(mappedVehicleViolation);
            
            
         //   var vehicle = await _bll.Vehicles.FirstOrDefaultAsync(vehicleViolation.VehicleId);
          //  vehicle!.Rating = (decimal) _bll.Vehicles.CalculateVehicleRatingByLicensePlate(vehicle!.RegNr!);
           // _bll.Vehicles.Update(vehicle);
            
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetVehicleViolation", new
            {
                version = HttpContext.GetRequestedApiVersion()?.ToString(),
                id = mappedVehicleViolation.Id
            }, vehicleViolation);
        }

        /// <summary>
        /// Delete vehicle violation by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [HttpDelete("delete/{id}")]
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
