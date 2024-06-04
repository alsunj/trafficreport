using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using App.Domain.Identity;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TrafficReport.Helpers;
using WebApp;

namespace TrafficReport.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class EvidenceController : ControllerBase
    {
        private readonly appSettings _appSettings;
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Evidence, App.BLL.DTO.Evidence> _mapper; 
        private readonly UserManager<AppUser> _userManager;
        private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");



        public EvidenceController(IAppBLL bll, IMapper autoMapper, UserManager<AppUser> userManager, appSettings appSettings)
        {
            _appSettings = appSettings;
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Evidence, App.BLL.DTO.Evidence>(autoMapper);
        }

        /// <summary>
        /// Get all evidences.
        /// </summary>
        /// <returns>List of evidences.</returns>
        [HttpGet("GetEvidences")]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.Evidence>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.Evidence>>> GetUserEvidences()
        { 
            var evidences = await _bll.Evidences.GetAllAsync();

            if (evidences.IsNullOrEmpty())
            {
                return NotFound();
            }    
            var baseUrl = _appSettings.BaseUrl;
            var fullUrlEvidences = evidences.Select(evidence =>
            {
                evidence.File = new Uri(new Uri(baseUrl), evidence.File).ToString();
                return evidence;
            }).ToList();
            

            return Ok(fullUrlEvidences);
        }

        /// <summary>
        /// Get evidences by Vehicle violation id.
        /// </summary>
        /// <param name="vehicleViolationId"></param>
        /// <returns>List of evidences.</returns>
        [HttpGet("GetAllEvidencesByVehicleViolationId/{vehicleViolationId}")]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.Evidence>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.Evidence>>> GetAllEvidenceForVehicleViolation(Guid vehicleViolationId)
        {
            var evidences = await _bll.Evidences.GetAllViolationEvidencesSortedAsync(vehicleViolationId);

            if (evidences.IsNullOrEmpty())
            {
                return NotFound();
            }           
            
            var baseUrl = _appSettings.BaseUrl;
            
            var fullUrlEvidences = evidences.Select(evidence =>
            {
                evidence.File = new Uri(new Uri(baseUrl), evidence.File).ToString();
                Console.WriteLine( evidence.File);
                return evidence;
            }).ToList();

            return Ok(fullUrlEvidences);
        }
    

        /// <summary>
        /// Edit evidence.
        /// </summary>
        /// <param name="id">Evidence id</param>
        /// <param name="evidence"></param>
        /// <returns></returns>
        [HttpPut("put/{id}")]
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
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        /// <summary>
        /// Add new evidence.
        /// </summary>
        ///         /// <param name="file"></param>

        /// <param name="evidence"></param>
        /// <returns></returns>
        [HttpPost("post")]
        [ProducesResponseType(typeof(App.DTO.v1_0.Evidence),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("multipart/form-data")]

        public async Task<ActionResult<App.DTO.v1_0.Evidence>> PostEvidence([FromForm] IFormFile file, [FromForm] App.DTO.v1_0.Evidence evidence)
        {
            try
            {
                if (file.Length > 0 && file.Length <= 25 * 1024 * 1024) // 25MB limit
                {
                    if (!Directory.Exists(_uploadPath))
                    {
                        Directory.CreateDirectory(_uploadPath);
                    }
        
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(_uploadPath, fileName);
        
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
        
                    evidence.Id = Guid.NewGuid();
                    evidence.File = Path.Combine("/uploads", fileName).Replace("~", ""); // Remove tilde (~)
        
                    var mappedEvidence = _mapper.Map(evidence);
                    _bll.Evidences.Add(mappedEvidence);
                    await _bll.SaveChangesAsync();
        
                    return CreatedAtAction("GetAllEvidenceForVehicleViolation", new { id = mappedEvidence.VehicleViolationId }, evidence);
                }
                else
                {
                    return BadRequest("File is too large or empty.");
                }
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error in PostEvidence method: {ex.Message}");
                // Return an error response
                return StatusCode(500, "Internal server error");
            }
        }


        /// <summary>
        /// Delete evidence by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [HttpDelete("delete/{id}")]
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

