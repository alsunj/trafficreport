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
    [ApiVersion("0.9", Deprecated = true)]

    [ApiController]
    [Route("api/v{version:ApiVersion}/violations/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ViolationTypeController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.ViolationType, App.BLL.DTO.ViolationType> _mapper;
        public ViolationTypeController(AppDbContext context, IAppBLL bll, UserManager<AppUser> userManager,
            IMapper autoMapper) 
        {
            _context = context;
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.ViolationType, App.BLL.DTO.ViolationType>(autoMapper);
        }

        // GET: api/ViolationType
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.ViolationType>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.ViolationType>>> GetViolationTypes()
        {
            var res = (await _bll.ViolationTypes.GetAllSortedAsync(
                    Guid.Parse(_userManager.GetUserId(User))
                ))
                .Select(e => _mapper.Map(e))
                .ToList();
            return Ok(res);
        }

        // GET: api/ViolationType/5
        [HttpGet("{id}")]
        [ProducesResponseType<ViolationType>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<ViolationType>> GetViolationType(Guid id)
        {
            var violationType = await _context.ViolationTypes.FindAsync(id);

            if (violationType == null)
            {
                return NotFound();
            }

            return violationType;
        }

        // PUT: api/ViolationType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutViolationType(Guid id, ViolationType violationType)
        {
            if (id != violationType.Id)
            {
                return BadRequest();
            }

            _context.Entry(violationType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViolationTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ViolationType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<ViolationType>((int) HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<ViolationType>> PostViolationType(ViolationType violationType)
        {
            _context.ViolationTypes.Add(violationType);
            await _context.SaveChangesAsync();

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
            var violationType = await _context.ViolationTypes.FindAsync(id);
            if (violationType == null)
            {
                return NotFound();
            }

            _context.ViolationTypes.Remove(violationType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ViolationTypeExists(Guid id)
        {
            return _context.ViolationTypes.Any(e => e.Id == id);
        }
    }
}
