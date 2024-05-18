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
    [Route("api/v{version:apiVersion}/vehicles/[controller]/[action]")]
    public class EvidenceController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Evidence, App.BLL.DTO.Evidence> _mapper; 

        public EvidenceController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Evidence, App.BLL.DTO.Evidence>(autoMapper);
        }

        // GET: api/Evidence
        [HttpGet]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.Evidence>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.Evidence>>> GetEvidences()
        {
            var bllEvidenceResult = await _bll.Evidences.GetAllAsync();
            var bllEvidences = bllEvidenceResult.Select(e => _mapper.Map(e)).ToList();
            return Ok(bllEvidences);
        }

        // GET: api/Evidence/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.Evidence>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.Evidence>> GetEvidence(Guid id)
        {
            var evidence = await _bll.Evidences.FirstOrDefaultAsync(id);

            if (evidence == null)
            {
                return NotFound();
            }
            var res = _mapper.Map(evidence);

            return Ok(res);
        }

        // PUT: api/Evidence/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutEvidence(Guid id, App.DTO.v1_0.Evidence evidence)
        {
            if (id != evidence.Id)
            {
                return BadRequest("bad request");
            }

            if (!await _bll.Evidences.ExistsAsync(id))
            {
                return NotFound("id doesnt exist");

            }
            var res = _mapper.Map(evidence);
            _bll.Evidences.Update(res);
            return NoContent();
        }

        // POST: api/Evidence
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(App.DTO.v1_0.Evidence),(int)HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<Evidence>> PostEvidence(App.DTO.v1_0.Evidence evidence)
        {
            var mappedEvidenceTypes = _mapper.Map(evidence);
            _bll.Evidences.Add(mappedEvidenceTypes);

            return CreatedAtAction("GetEvidence", new { id = evidence.Id }, evidence);
        }

        // DELETE: api/Evidence/5
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")] 
        public async Task<IActionResult> DeleteEvidence(Guid id)
        {
            var evidence = await _bll.Evidences.FirstOrDefaultAsync(id);
            if (evidence == null)
            {
                return NotFound();
            }

            await _bll.Evidences.RemoveAsync(id);


            return NoContent();
        }

        private bool EvidenceExists(Guid id)
        {
            return _bll.Evidences.Exists(id);
        }
    }
}

