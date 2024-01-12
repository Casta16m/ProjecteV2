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
    public class ExtensioController : ControllerBase
    {
        private readonly DataContext _context;

        public ExtensioController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Extensio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Extensio>>> GetExtensio()
        {
            return await _context.Extensio.ToListAsync();
        }

        // GET: api/Extensio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Extensio>> GetExtensio(string id)
        {
            var extensio = await _context.Extensio.FindAsync(id);

            if (extensio == null)
            {
                return NotFound();
            }

            return extensio;
        }

        // PUT: api/Extensio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExtensio(string id, Extensio extensio)
        {
            if (id != extensio.NomExtensio)
            {
                return BadRequest();
            }

            _context.Entry(extensio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExtensioExists(id))
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

        // POST: api/Extensio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Extensio>> PostExtensio(Extensio extensio)
        {
            _context.Extensio.Add(extensio);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExtensioExists(extensio.NomExtensio))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetExtensio", new { id = extensio.NomExtensio }, extensio);
        }

        // DELETE: api/Extensio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExtensio(string id)
        {
            var extensio = await _context.Extensio.FindAsync(id);
            if (extensio == null)
            {
                return NotFound();
            }

            _context.Extensio.Remove(extensio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExtensioExists(string id)
        {
            return _context.Extensio.Any(e => e.NomExtensio == id);
        }
    }
}
