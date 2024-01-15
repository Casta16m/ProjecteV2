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
    public class LlistaController : ControllerBase
    {
        private readonly DataContext _context;

        public LlistaController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Llista
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Llista>>> GetLlista()
        {
            return await _context.Llista.ToListAsync();
        }

        // GET: api/Llista/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Llista>> GetLlista(string id)
        {
            var llista = await _context.Llista.FindAsync(id);

            if (llista == null)
            {
                return NotFound();
            }

            return llista;
        }

        // GET: api/Llista/getLlistaNom/Nom
        [HttpGet("BuscarNom/{nom}")]
        public async Task<ActionResult<Llista>> getLlistaNom(string nom)
        {
            var llista = await _context.Llista.Where(a => a.Nom.Contains(nom)).ToListAsync();

            if (llista == null)
            {
                return NotFound();
            }

            return llista.ToList()[0];
        }
    

        // PUT: api/Llista/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLlista(string id, Llista llista)
        {
            if (id != llista.Nom)
            {
                return BadRequest();
            }

            _context.Entry(llista).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LlistaExists(id))
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

        // POST: api/Llista
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Llista>> PostLlista(Llista llista)
        {
            _context.Llista.Add(llista);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LlistaExists(llista.Nom))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLlista", new { id = llista.Nom }, llista);
        }

        // DELETE: api/Llista/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLlista(string id)
        {
            var llista = await _context.Llista.FindAsync(id);
            if (llista == null)
            {
                return NotFound();
            }

            _context.Llista.Remove(llista);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LlistaExists(string id)
        {
            return _context.Llista.Any(e => e.Nom == id);
        }
    }
}
