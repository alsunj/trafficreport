using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using App.Domain.Identity;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TrafficReport.Helpers;

namespace TrafficReport.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/evidences/[controller]/[action]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class EvidenceController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Evidence, App.BLL.DTO.Evidence> _mapper; 
        private readonly UserManager<AppUser> _userManager;


        public EvidenceController(IAppBLL bll, IMapper autoMapper, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Evidence, App.BLL.DTO.Evidence>(autoMapper);
        }

        /// <summary>
        /// Get all evidences for current user.
        /// </summary>
        /// <returns>List of evidences.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.Evidence>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.Evidence>>> GetUserEvidences()
        { 
            var bllEvidenceResult = (await _bll.Evidences.GetAllSortedAsync(
                    Guid.Parse(_userManager.GetUserId(User))
                ))
                .Select(e => _mapper.Map(e))
                .ToList();
            return Ok(bllEvidenceResult);
        }

        /// <summary>
        /// Get evidences by Vehicle violation id.
        /// </summary>
        /// <param name="vehicleViolationId"></param>
        /// <returns>List of evidences.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.Evidence>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.Evidence>>> GetAllEvidenceForVehicleViolation(Guid vehicleViolationId)
        {
            var evidences = await _bll.Evidences.GetAllViolationEvidencesSortedAsync(vehicleViolationId);

            if (evidences.IsNullOrEmpty())
            {
                return NotFound();
            }
            
            return Ok(evidences);
        }

        /// <summary>
        /// Edit evidence.
        /// </summary>
        /// <param name="id">Evidence id</param>
        /// <param name="evidence"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
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
            // var userVehicleviolations =
            //     await _bll.VehicleViolations.GetAllSortedAsync(Guid.Parse(_userManager.GetUserId(User)));
            
            // if (!(userVehicleviolations.FirstOrDefault(violation => violation.Id.Equals(evidence.VehicleViolationId))!)
            //     .Equals(userVehicleviolations.FirstOrDefault(violation => violation.AppUserId.Equals(Guid.Parse(_userManager.GetUserId(User))))))
            // {
            //     return Unauthorized();
            // }
            
            var res = _mapper.Map(evidence);
            _bll.Evidences.Update(res);
            return NoContent();
        }

        /// <summary>
        /// Add new evidence.
        /// </summary>
        /// <param name="evidence"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(App.DTO.v1_0.Evidence),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.Evidence>> PostEvidence(App.DTO.v1_0.Evidence evidence)
        {
            var mappedEvidenceTypes = _mapper.Map(evidence);
            _bll.Evidences.Add(mappedEvidenceTypes);

            return CreatedAtAction("GetAllEvidenceForVehicleViolation", new { id = evidence.VehicleViolationId }, evidence);
        }

        /// <summary>
        /// Delete evidence by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

