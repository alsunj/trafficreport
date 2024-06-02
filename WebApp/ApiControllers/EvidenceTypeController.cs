using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using App.Domain.Evidences;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using TrafficReport.Helpers;

namespace TrafficReport.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class EvidenceTypeController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.EvidenceType, App.BLL.DTO.EvidenceType> _mapper; 
        

        public EvidenceTypeController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.EvidenceType, App.BLL.DTO.EvidenceType>(autoMapper);
        }

        /// <summary>
        /// Get all evidence types.
        /// </summary>
        /// <returns>List of evidence types.</returns>
        [HttpGet("GetEvidenceTypes")]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.EvidenceType>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.EvidenceType>>> GetEvidenceTypes()
        {
            var bllEvidenceTypeResult = await _bll.EvidenceTypes.GetAllAsync();
            var bllEvidenceTypes = bllEvidenceTypeResult.Select(e => _mapper.Map(e)).ToList();
            return Ok(bllEvidenceTypes);
        }

        /// <summary>
        /// Get evidence type by id.
        /// </summary>
        /// <param name="id">Evidence type id.</param>
        /// <returns>Evidence type</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(App.DTO.v1_0.EvidenceType),(int)HttpStatusCode.OK)]
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

        /// <summary>
        /// Edit evidence type.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="evidenceType"></param>
        /// <returns></returns>
        [HttpPut("put/{id}")]
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
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Add evidence type.
        /// </summary>
        /// <param name="evidenceType"></param>
        /// <returns></returns>
        [HttpPost("post")]
        [ProducesResponseType(typeof(App.DTO.v1_0.EvidenceType),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.EvidenceType>> PostEvidenceType(App.DTO.v1_0.EvidenceType evidenceType)
        {
            evidenceType.Id = Guid.NewGuid();
            var mappedEvidenceType = _mapper.Map(evidenceType);
            _bll.EvidenceTypes.Add(mappedEvidenceType);
            await _bll.SaveChangesAsync();
            
            return CreatedAtAction("GetEvidenceType", new { id = mappedEvidenceType.Id }, evidenceType);
        }

        /// <summary>
        /// Delete evidence type by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [HttpDelete("delete/{id}")]
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
