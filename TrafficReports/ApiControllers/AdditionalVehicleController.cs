using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Vehicles;
using Asp.Versioning;
using AutoMapper;
using TrafficReport.Helpers;

namespace TrafficReports.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/vehicles/[controller]/[action]")]
    public class AdditionalVehicleController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.AdditionalVehicle, App.BLL.DTO.AdditionalVehicle> _mapper; 

        public AdditionalVehicleController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.AdditionalVehicle, App.BLL.DTO.AdditionalVehicle>(autoMapper);
        }

        // GET: api/AdditionalVehicle
        [HttpGet]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.AdditionalVehicle>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.AdditionalVehicle>>> GetAdditionalVehicles()
        {
            var bllAdditionalVehicleResult = await _bll.AdditionalVehicles.GetAllAsync();
            var bllAdditionalVehicles = bllAdditionalVehicleResult.Select(e =>_mapper.Map(e)).ToList();
            return Ok(bllAdditionalVehicles);
        }

        // GET: api/AdditionalVehicle/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.AdditionalVehicle>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
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

        // PUT: api/AdditionalVehicle/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
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
            return NoContent();
        }

        // POST: api/AdditionalVehicle
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(App.DTO.v1_0.AdditionalVehicle),(int)HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<AdditionalVehicle>> PostAdditionalVehicle(App.DTO.v1_0.AdditionalVehicle additionalVehicle)
        {
            var mappedAdditionalVehicles = _mapper.Map(additionalVehicle);
            _bll.AdditionalVehicles.Add(mappedAdditionalVehicles);

            return CreatedAtAction("GetAdditionalVehicle", new { id = additionalVehicle.Id }, additionalVehicle);
        }

        // DELETE: api/AdditionalVehicle/5
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]
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

        private bool AdditionalVehicleExists(Guid id)
        {
            return _bll.AdditionalVehicles.Exists(id);
        }
    }
}
