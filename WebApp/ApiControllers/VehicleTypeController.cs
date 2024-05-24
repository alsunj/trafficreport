using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using TrafficReport.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;


namespace TrafficReport.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/vehicles/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VehicleTypeController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.VehicleType, App.BLL.DTO.VehicleType> _mapper;

        public VehicleTypeController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.VehicleType,App.BLL.DTO.VehicleType>(autoMapper);
        }

        /// <summary>
        /// Get all vehicle types.
        /// </summary>
        /// <returns>List of vehicle types.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.VehicleType>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.VehicleType>>> GetVehicleTypes()
        {
            var bllVehicleTypesResult = await _bll.VehicleTypes.GetAllAsync();
            var bllVehicleTypes = bllVehicleTypesResult.Select(e => _mapper.Map(e)).ToList();
            
            return Ok(bllVehicleTypes);
        }

        /// <summary>
        /// Get vehicle type by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>VehicleType</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(App.DTO.v1_0.VehicleType),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.VehicleType>> GetVehicleType(Guid id)
        {
            var vehicleType = await _bll.VehicleTypes.FirstOrDefaultAsync(id);

            if (vehicleType == null)
            {
                return NotFound();
            }

            var res = _mapper.Map(vehicleType);
            return Ok(res);
        }

        /// <summary>
        /// Edit vehicle type.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vehicleType"></param>
        /// <returns></returns>
        [HttpPut("{vehicleTypeId}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutVehicleType(Guid vehicleTypeId, App.DTO.v1_0.VehicleType vehicleType)
        {
            if (vehicleTypeId != vehicleType.Id)
            {
                return BadRequest("bad request");
            }
            if (!await _bll.VehicleTypes.ExistsAsync(vehicleTypeId))
            {
                return NotFound("id doesnt exist");
            }

            var res = _mapper.Map(vehicleType);
            _bll.VehicleTypes.Update(res);
            await _bll.SaveChangesAsync();


            return NoContent();
        }

        /// <summary>
        /// Add vehicle type.
        /// </summary>
        /// <param name="vehicleType"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(App.DTO.v1_0.VehicleType),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.VehicleType>> PostVehicleType(App.DTO.v1_0.VehicleType vehicleType)
        {
            vehicleType.Id = Guid.NewGuid();
            var mappedVehicleType = _mapper.Map(vehicleType);
            _bll.VehicleTypes.Add(mappedVehicleType);
            await _bll.SaveChangesAsync();
            
            return CreatedAtAction("GetVehicleType", new 
                { id = mappedVehicleType.Id }, vehicleType);
        }

        /// <summary>
        /// Delete vehicle type by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")] 
        public async Task<IActionResult> DeleteVehicleType(Guid id)
        {
            var vehicleType = await _bll.VehicleTypes.FirstOrDefaultAsync(id);
            
            if (vehicleType == null)
            {
                return NotFound();
            }

            await _bll.VehicleTypes.RemoveAsync(id);

            return NoContent();
        }

        private bool VehicleTypeExists(Guid id)
        {
            return _bll.VehicleTypes.Exists(id);
        }
    }
}
