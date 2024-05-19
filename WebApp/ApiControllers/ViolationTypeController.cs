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

using Asp.Versioning;
using AutoMapper;
using TrafficReport.Helpers;

namespace TrafficReports.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/violations/[controller]/[action]")]
    public class ViolationTypeController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.ViolationType, App.BLL.DTO.ViolationType> _mapper;


        public ViolationTypeController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.ViolationType, App.BLL.DTO.ViolationType>(autoMapper);

        }

          // GET: api/ViolationType
          [HttpGet]
          [ProducesResponseType(typeof(List<App.DTO.v1_0.ViolationType>),(int)HttpStatusCode.OK)]
          [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
          [Produces("application/json")]
          [Consumes("application/json")]
      
          public async Task<ActionResult<List<App.DTO.v1_0.ViolationType>>> GetViolationTypes()
          {
              var bllViolationTypesResult = await _bll.ViolationTypes.GetAllAsync();
              var bllViolationTypes = bllViolationTypesResult.Select(e => _mapper.Map(e)).ToList();
              return Ok(bllViolationTypes);

          }

        // GET: api/ViolationType/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(App.DTO.v1_0.ViolationType),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.ViolationType>> GetViolationType(Guid id)
        {
            var violationType = await _bll.ViolationTypes.FirstOrDefaultAsync(id);

            if (violationType == null)
            {
                return NotFound();
            }

            var res = _mapper.Map(violationType);

            return Ok(res);
        }

        // PUT: api/ViolationType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutViolationType(Guid id, App.DTO.v1_0.ViolationType violationType)
        {
            if (id != violationType.Id)
            {
                return BadRequest("bad request");
            }

            if (!await _bll.ViolationTypes.ExistsAsync(id))
            {
                return NotFound("id doesnt exist");
            }
            var res = _mapper.Map(violationType);
            
            _bll.ViolationTypes.Update(res);
            //mapper?
            
            return NoContent();
        }

        // POST: api/ViolationType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(App.DTO.v1_0.ViolationType),(int)HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.ViolationType>> PostViolationType(App.DTO.v1_0.ViolationType violationType)
        {
            var mappedViolationTypes = _mapper.Map(violationType);
            _bll.ViolationTypes.Add(mappedViolationTypes);
            
            return CreatedAtAction("GetViolationType", new
            {
                version = HttpContext.GetRequestedApiVersion()?.ToString(),
                id = violationType.Id 
                
            }, violationType);
        }

        // DELETE: api/ViolationType/5
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")] 
        public async Task<IActionResult> DeleteViolationType(Guid id)
        {
            var violationType = await _bll.ViolationTypes.FirstOrDefaultAsync(id);
            
            if (violationType == null)
            {
                return NotFound();
            }

            await _bll.ViolationTypes.RemoveAsync(id);
            return NoContent();
        }
        
        private bool ViolationTypeExists(Guid id)
        {
            return _bll.ViolationTypes.Exists(id);
        }
        
    }
}
