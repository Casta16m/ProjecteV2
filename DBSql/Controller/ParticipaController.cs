using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjecteV2.ApiSql;

namespace DBSql.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipaController : ControllerBase
    {
        private readonly DataContext _context;

        public ParticipaController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Participa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participa>>> GetParticipa()
        {
            return await _context.Participa.ToListAsync();
        }

        // GET: api/Participa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Participa>> GetParticipa(string id)
        {
            var participa = await _context.Participa.FindAsync(id);

            if (participa == null)
            {
                return NotFound();
            }

            return participa;
        }

        // PUT: api/Participa/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipa(string id, Participa participa)
        {
            if (id != participa.UID)
            {
                return BadRequest();
            }

            _context.Entry(participa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipaExists(id))
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

        // POST: api/Participa
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Participa>> PostParticipa(Participa participa)
        {
            _context.Participa.Add(participa);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ParticipaExists(participa.UID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetParticipa", new { id = participa.UID }, participa);
        }

        // DELETE: api/Participa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipa(string id)
        {
            var participa = await _context.Participa.FindAsync(id);
            if (participa == null)
            {
                return NotFound();
            }

            _context.Participa.Remove(participa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParticipaExists(string id)
        {
            return _context.Participa.Any(e => e.UID == id);
        }
    }
}
