using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Identity;
using App.Domain.Violations;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TrafficReport.Helpers;

namespace TrafficReports.ApiControllers

{

    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/violations/[controller]/[action]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ViolationController : ControllerBase
    {

        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Violation, App.BLL.DTO.Violation> _mapper;

        public ViolationController(IAppBLL bll, UserManager<AppUser> userManager, IMapper autoMapper)
        {
            
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Violation, App.BLL.DTO.Violation>(autoMapper);
        }
        // GET: api/Violation
        [HttpGet]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.Violation>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.Violation>>> GetViolations()
        {
            var bllViolationResult = await _bll.Violations.GetAllAsync();
            var bllViolations = bllViolationResult.Select(e => _mapper.Map(e)).ToList();
            return Ok(bllViolations);
        }

        // GET: api/Violation/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.Violation>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.Violation>> GetViolation(Guid id)
        {
            var violation = await _bll.Violations.FirstOrDefaultAsync(id);

            if (violation == null)
            {
                return NotFound();
            }

            var res = _mapper.Map(violation);

            return Ok(res);
        }

        // PUT: api/Violation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutViolation(Guid id, App.DTO.v1_0.Violation violation)
        {
            if (id != violation.Id)
            {
                return BadRequest("bad request");
            }

            if (!await _bll.Violations.ExistsAsync(id))
            {
                return NotFound("id doesnt exist");
            }

            var res = _mapper.Map(violation);

            _bll.Violations.Update(res);
            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/Violation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(List<App.DTO.v1_0.Violation>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.Violation>> PostViolation(App.DTO.v1_0.Violation violation)
        {
            var mappedViolation = _mapper.Map(violation);
            _bll.Violations.Add(mappedViolation);
            await _bll.SaveChangesAsync();
            
            return CreatedAtAction("GetViolation", new
            {
                version = HttpContext.GetRequestedApiVersion()?.ToString(),
                id = mappedViolation.Id
            }, violation);
        }

        // DELETE: api/Violation/5
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteViolation(Guid id)
        {
            var violation = await _bll.Violations.FirstOrDefaultAsync(id);
            if (violation == null)
            {
                return NotFound();
            }

            await _bll.Violations.RemoveAsync(id);
            return NoContent();
        }

        private bool ViolationExists(Guid id)
        {
            return _bll.Violations.Exists(id);
        }
    }
}
