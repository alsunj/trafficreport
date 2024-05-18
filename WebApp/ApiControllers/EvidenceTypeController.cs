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
using App.Domain.Evidences;
using Asp.Versioning;
using AutoMapper;
using TrafficReport.Helpers;

namespace TrafficReports.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/evidences/[controller]/[action]")]
    public class EvidenceTypeController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.EvidenceType, App.BLL.DTO.EvidenceType> _mapper; 

        public EvidenceTypeController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.EvidenceType, App.BLL.DTO.EvidenceType>(autoMapper);
        }

        // GET: api/EvidenceType
        [HttpGet]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.EvidenceType>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.EvidenceType>>> GetEvidenceTypes()
        {
            var bllEvidenceTypeResult = await _bll.EvidenceTypes.GetAllAsync();
            var bllEvidenceTypes = bllEvidenceTypeResult.Select(e => _mapper.Map(e)).ToList();
            return Ok(bllEvidenceTypes);
        }

        // GET: api/EvidenceType/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.EvidenceType>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.EvidenceType>> GetEvidenceType(Guid id)
        {
            var evidenceType = await _bll.EvidenceTypes.FirstOrDefaultAsync(id);

            if (evidenceType == null)
            {
                return NotFound();
            }

            var res = _mapper.Map(evidenceType);
            return Ok(res);
        }

        // PUT: api/EvidenceType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutEvidenceType(Guid id, App.DTO.v1_0.EvidenceType evidenceType)
        {
            if (id != evidenceType.Id)
            {
                return BadRequest("bad request");
            }

            if (!await _bll.EvidenceTypes.ExistsAsync(id))
            {
                return NotFound("id doesnt exist");

            }

            var res = _mapper.Map(evidenceType);
            _bll.EvidenceTypes.Update(res);

            return NoContent();
        }

        // POST: api/EvidenceType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(App.DTO.v1_0.EvidenceType),(int)HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<EvidenceType>> PostEvidenceType(App.DTO.v1_0.EvidenceType evidenceType)
        {
            var mappedEvidenceTypes = _mapper.Map(evidenceType);
            _bll.EvidenceTypes.Add(mappedEvidenceTypes);
            

            return CreatedAtAction("GetEvidenceType", new { id = evidenceType.Id }, evidenceType);
        }

        // DELETE: api/EvidenceType/5
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")] 
        public async Task<IActionResult> DeleteEvidenceType(Guid id)
        {
            var evidenceType = await _bll.EvidenceTypes.FirstOrDefaultAsync(id);
            if (evidenceType == null)
            {
                return NotFound();
            }

            await _bll.EvidenceTypes.RemoveAsync(id);

            return NoContent();
        }

        private bool EvidenceTypeExists(Guid id)
        {
            return  _bll.EvidenceTypes.Exists(id);
        }
    }
}
