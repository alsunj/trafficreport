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
using TrafficReport.Helpers;
using AutoMapper;


namespace TrafficReports.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/vehicles/[controller]/[action]")]
    public class VehicleTypeController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.VehicleType, App.BLL.DTO.VehicleType> _mapper;

        public VehicleTypeController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.VehicleType,App.BLL.DTO.VehicleType>(autoMapper);
        }

        // GET: api/VehicleType
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

        // GET: api/VehicleType/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.VehicleType>),(int)HttpStatusCode.OK)]
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

        // PUT: api/VehicleType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutVehicleType(Guid id, App.DTO.v1_0.VehicleType vehicleType)
        {
            if (id != vehicleType.Id)
            {
                return BadRequest("bad request");
            }
            if (!await _bll.VehicleTypes.ExistsAsync(id))
            {
                return NotFound("id doesnt exist");
            }

            var res = _mapper.Map(vehicleType);
            _bll.VehicleTypes.Update(res);
            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/VehicleType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(App.DTO.v1_0.VehicleType),(int)HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.VehicleType>> PostVehicleType(App.DTO.v1_0.VehicleType vehicleType)
        {
            //vehicleType.Id = new Guid();
            var mappedVehicleType = _mapper.Map(vehicleType);
            _bll.VehicleTypes.Add(mappedVehicleType);
            await _bll.SaveChangesAsync();
            
            return CreatedAtAction("GetVehicleType", new 
                { id = mappedVehicleType.Id }, vehicleType);
        }

        // DELETE: api/VehicleType/5
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
