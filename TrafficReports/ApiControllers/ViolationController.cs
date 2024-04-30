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

    public class ViolationController : ControllerBase
    {
        
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Violation, App.BLL.DTO.Violation> _mapper;

        public ViolationController(AppDbContext context, IAppBLL bll, UserManager<AppUser> userManager,
            IMapper autoMapper, PublicDTOBllMapper<App.DTO.v1_0.Violation, App.BLL.DTO.Violation> mapper)
        {
            _context = context;
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Violation, App.BLL.DTO.Violation>(autoMapper);
        }
        // GET: api/Violation
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.Violation>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<List<Violation>> GetViolations()
        {
            return await _context.Violations.ToListAsync();
        }

        // GET: api/Violation/5
        [HttpGet("{id}")]
        [ProducesResponseType<Violation>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<Violation>> GetViolation(Guid id)
        {
            var violation = await _context.Violations.FindAsync(id);

            if (violation == null)
            {
                return NotFound();
            }

            return violation;
        }

        // PUT: api/Violation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutViolation(Guid id, Violation violation)
        {
            if (id != violation.Id)
            {
                return BadRequest();
            }

            _context.Entry(violation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViolationExists(id))
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

        // POST: api/Violation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<Violation>((int) HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<Violation>> PostViolation(Violation violation)
        {
            _context.Violations.Add(violation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetViolation", new
            {
                version = HttpContext.GetRequestedApiVersion()?.ToString(),
                id = violation.Id
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
            var violation = await _context.Violations.FindAsync(id);
            if (violation == null)
            {
                return NotFound();
            }

            _context.Violations.Remove(violation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ViolationExists(Guid id)
        {
            return _context.Violations.Any(e => e.Id == id);
        }
    }
}
