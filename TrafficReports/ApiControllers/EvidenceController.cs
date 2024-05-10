using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Evidences;

namespace TrafficReports.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvidenceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EvidenceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Evidence
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evidence>>> GetEvidences()
        {
            return await _context.Evidences.ToListAsync();
        }

        // GET: api/Evidence/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Evidence>> GetEvidence(Guid id)
        {
            var evidence = await _context.Evidences.FindAsync(id);

            if (evidence == null)
            {
                return NotFound();
            }

            return evidence;
        }

        // PUT: api/Evidence/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvidence(Guid id, Evidence evidence)
        {
            if (id != evidence.Id)
            {
                return BadRequest();
            }

            _context.Entry(evidence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvidenceExists(id))
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

        // POST: api/Evidence
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Evidence>> PostEvidence(Evidence evidence)
        {
            _context.Evidences.Add(evidence);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvidence", new { id = evidence.Id }, evidence);
        }

        // DELETE: api/Evidence/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvidence(Guid id)
        {
            var evidence = await _context.Evidences.FindAsync(id);
            if (evidence == null)
            {
                return NotFound();
            }

            _context.Evidences.Remove(evidence);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EvidenceExists(Guid id)
        {
            return _context.Evidences.Any(e => e.Id == id);
        }
    }
}
