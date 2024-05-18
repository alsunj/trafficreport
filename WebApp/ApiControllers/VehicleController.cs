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
    public class VehicleController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Vehicle, App.BLL.DTO.Vehicle> _mapper;

        public VehicleController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Vehicle,App.BLL.DTO.Vehicle>(autoMapper);
        }

        // GET: api/Vehicle
        [HttpGet]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.Vehicle>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.Vehicle>>> GetVehicles()
        {
            var bllVehicleControllerResult = await _bll.Vehicles.GetAllAsync();
            var bllVehicleController = bllVehicleControllerResult.Select(e => _mapper.Map(e)).ToList();
            return Ok(bllVehicleController);
        }

        // GET: api/Vehicle/5
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

        // PUT: api/Vehicle/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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


            return NoContent();
        }

        // POST: api/Vehicle
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(App.DTO.v1_0.Vehicle),(int)HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.Vehicle>> PostVehicle(App.DTO.v1_0.Vehicle vehicle)
        {
            var mappedVehicles = _mapper.Map(vehicle);
            _bll.Vehicles.Add(mappedVehicles);

            return CreatedAtAction("GetVehicle", new { id = vehicle.Id }, vehicle);
        }

        // DELETE: api/Vehicle/5
        [HttpDelete("{id}")]
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

        private bool VehicleExists(Guid id)
        {
            return _bll.Vehicles.Exists(id);
        }
    }
}

