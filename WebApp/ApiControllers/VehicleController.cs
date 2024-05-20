using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using AutoMapper;
using TrafficReport.Helpers;

namespace TrafficReport.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/vehicles/[controller]/[action]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class VehicleController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Vehicle, App.BLL.DTO.Vehicle> _mapper;

        public VehicleController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Vehicle,App.BLL.DTO.Vehicle>(autoMapper);
        }

        /// <summary>
        /// Get all vehicles.
        /// </summary>
        /// <returns>List of vehicles.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.Vehicle>),(int)HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.Vehicle>>> GetVehicles()
        {
            var bllVehicleControllerResult = await _bll.Vehicles.GetAllAsync();
            var bllVehicleController = bllVehicleControllerResult.Select(e => _mapper.Map(e)).ToList();
            return Ok(bllVehicleController);
        }

        /// <summary>
        /// Get vehicle by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Vehicle</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.Vehicle>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.Vehicle>> GetVehicle(Guid id)
        {
            var vehicle = await _bll.Vehicles.FirstOrDefaultAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            var res = _mapper.Map(vehicle);
            return Ok(res);
        }

        /// <summary>
        /// Get vehicle by license plate.
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <returns>Vehicle</returns>
        [HttpGet("licensePlate")]
        [ProducesResponseType(typeof(App.DTO.v1_0.Vehicle), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.Vehicle>> GetVehicleByLicensePlate(string licensePlate)
        {
            var vehicle = await _bll.Vehicles.GetByLicensePlateAsync(licensePlate);

            if (vehicle == null)
            {
                return NotFound();
            }
            var res = _mapper.Map(vehicle);
            return Ok(res);
        }

        /// <summary>
        /// Edit vehicle.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutVehicle(Guid id, App.DTO.v1_0.Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest("bad request");
            }

            if (!await _bll.Vehicles.ExistsAsync(id))
            {
                return NotFound("id doesnt exist");

            }
            var res = _mapper.Map(vehicle);
            _bll.Vehicles.Update(res);
            await _bll.SaveChangesAsync();


            return NoContent();
        }

        /// <summary>
        /// Add vehicle.
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(App.DTO.v1_0.Vehicle),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.Vehicle>> PostVehicle(App.DTO.v1_0.Vehicle vehicle)
        {
            vehicle.Id = Guid.NewGuid();
            var mappedVehicle = _mapper.Map(vehicle);
            _bll.Vehicles.Add(mappedVehicle);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetVehicle", new { id = mappedVehicle.Id }, vehicle);
        }

        /// <summary>
        /// Delete vehicle by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType ((int)HttpStatusCode.NoContent)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteVehicle(Guid id)
        {
            var vehicle = await _bll.Vehicles.FirstOrDefaultAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            await _bll.Vehicles.RemoveAsync(id);
            
            return NoContent();
        }
        
        /// <summary>
        /// Delete vehicle by license plate.
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <returns></returns>
        [HttpDelete("{licensePlate}")]
        [ProducesResponseType ((int)HttpStatusCode.NoContent)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteVehicleByLicensePlate(string licensePlate)
        {
            var vehicle = await _bll.Vehicles.GetByLicensePlateAsync(licensePlate);
            if (vehicle == null)
            {
                return NotFound();
            }

            await _bll.Vehicles.RemoveAsync(vehicle.Id);
            
            return NoContent();
        }

        private bool VehicleExists(Guid id)
        {
            return _bll.Vehicles.Exists(id);
        }
    }
}

