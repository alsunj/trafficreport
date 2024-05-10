using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Evidences;
/*
namespace TrafficReports.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvidenceTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EvidenceTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/EvidenceType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvidenceType>>> GetEvidenceTypes()
        {
            return await _context.EvidenceTypes.ToListAsync();
        }

        // GET: api/EvidenceType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EvidenceType>> GetEvidenceType(Guid id)
        {
            var evidenceType = await _context.EvidenceTypes.FindAsync(id);

            if (evidenceType == null)
            {
                return NotFound();
            }

            return evidenceType;
        }

        // PUT: api/EvidenceType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvidenceType(Guid id, EvidenceType evidenceType)
        {
            if (id != evidenceType.Id)
            {
                return BadRequest();
            }

            _context.Entry(evidenceType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvidenceTypeExists(id))
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

        // POST: api/EvidenceType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EvidenceType>> PostEvidenceType(EvidenceType evidenceType)
        {
            _context.EvidenceTypes.Add(evidenceType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvidenceType", new { id = evidenceType.Id }, evidenceType);
        }

        // DELETE: api/EvidenceType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvidenceType(Guid id)
        {
            var evidenceType = await _context.EvidenceTypes.FindAsync(id);
            if (evidenceType == null)
            {
                return NotFound();
            }

            _context.EvidenceTypes.Remove(evidenceType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EvidenceTypeExists(Guid id)
        {
            return _context.EvidenceTypes.Any(e => e.Id == id);
        }
    }
}
*/