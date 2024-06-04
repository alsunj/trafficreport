using System.Net;
using App.Contracts.BLL;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrafficReport.Helpers;

namespace TrafficReport.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v{version:apiVersion}/[controller]/")]
    public class AdditionalVehicleController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.AdditionalVehicle, App.BLL.DTO.AdditionalVehicle> _mapper; 

        public AdditionalVehicleController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.AdditionalVehicle, App.BLL.DTO.AdditionalVehicle>(autoMapper);
        }

        /// <summary>
        /// Get additional vehicles
        /// </summary>
        /// <returns>List of additional vehicles</returns>
        [HttpGet("GetAdditionalVehicles")]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.AdditionalVehicle>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.AdditionalVehicle>>> GetAdditionalVehicles()
        {
            var bllAdditionalVehicleResult = await _bll.AdditionalVehicles.GetAllAsync();
            var bllAdditionalVehicles = bllAdditionalVehicleResult.Select(e =>_mapper.Map(e)).ToList();
            return Ok(bllAdditionalVehicles);
        }

        /// <summary>
        /// Get additional vehicle by id.
        /// </summary>
        /// <param name="id">Additional vehicle id.</param>
        /// <returns>App.DTO.v1_0.AdditionalVehicle</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(App.DTO.v1_0.AdditionalVehicle),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.AdditionalVehicle>> GetAdditionalVehicle(Guid id)
        {
            var additionalVehicle = await _bll.AdditionalVehicles.FirstOrDefaultAsync(id);

            if (additionalVehicle == null)
            {
                return NotFound();
            }

            var res = _mapper.Map(additionalVehicle);

            return Ok(res);
        }
        /// <summary>
        /// Get additional vehicle by id.
        /// </summary>
        /// <param name="id">Additional vehicle id.</param>
        /// <returns>App.DTO.v1_0.AdditionalVehicle</returns>
        [HttpGet("GetAdditionalVehicleByVehicleViolation/{vehicleViolationId}")]
        [ProducesResponseType(typeof(App.DTO.v1_0.AdditionalVehicle),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]

        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.AdditionalVehicle>> GetAdditionalVehicleByVehicleViolation(Guid vehicleViolationId)
        {
            var additionalVehicles = await _bll.AdditionalVehicles.GetAllByVehicleViolationSortedAsync(vehicleViolationId);

            if (additionalVehicles == null)
            {
                return NotFound();
            }

            additionalVehicles = additionalVehicles.ToList();

            return Ok(additionalVehicles);
        }

        /// <summary>
        /// Edit Additional vehicle.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="additionalVehicle"></param>
        /// <returns></returns>
        [HttpPut("put/{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutAdditionalVehicle(Guid id, App.DTO.v1_0.AdditionalVehicle additionalVehicle)
        {
            if (id != additionalVehicle.Id)
            {
                return BadRequest();
            }

            if (!await _bll.AdditionalVehicles.ExistsAsync(id))
            {
                return NotFound("id doesnt exist");

            }

            var res = _mapper.Map(additionalVehicle);
            _bll.AdditionalVehicles.Update(res);
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Add additional vehicle.
        /// </summary>
        /// <param name="additionalVehicle">AdditionalVehicle</param>
        /// <returns>App.DTO.v1_0.AdditionalVehicle</returns>
        [HttpPost("post")]
        [ProducesResponseType(typeof(App.DTO.v1_0.AdditionalVehicle),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.AdditionalVehicle>> PostAdditionalVehicle(App.DTO.v1_0.AdditionalVehicle additionalVehicle)
        {
            additionalVehicle.Id = Guid.NewGuid();
            var mappedAdditionalVehicle = _mapper.Map(additionalVehicle);
            _bll.AdditionalVehicles.Add(mappedAdditionalVehicle);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetAdditionalVehicle", new { id = mappedAdditionalVehicle.Id }, additionalVehicle);
        }

        /// <summary>
        /// Delete additional vehicle.
        /// </summary>
        /// <param name="id">Additional Vehicle id</param>
        /// <returns></returns>
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [HttpDelete("delete/{id}")]
        [Produces("application/json")]
        [Consumes("application/json")] 
        public async Task<IActionResult> DeleteAdditionalVehicle(Guid id)
        {
            var additionalVehicle = await _bll.AdditionalVehicles.FirstOrDefaultAsync(id);
            if (additionalVehicle == null)
            {
                return NotFound();
            }

            await _bll.AdditionalVehicles.RemoveAsync(id);

            return NoContent();
        }

        /// <summary>
        /// checks, whether additional vehicle exists
        /// </summary>
        /// <param name="id">Additional vehicle id</param>
        /// <returns>bool</returns>
        private bool AdditionalVehicleExists(Guid id)
        {
            return _bll.AdditionalVehicles.Exists(id);
        }
    }
}
