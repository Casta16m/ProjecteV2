/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjecteV2.ApiSql;
using ProjecteV2.ApiSql.Services;

namespace DBSql.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtensioController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ExtensioService _extensioService;

        public ExtensioController(DataContext context)
        {
            _context = context;
            _extensioService = new ExtensioService(context);
        }

        // GET: api/Extensio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Extensio>>> GetExtensio()
        {
            return await _context.Extensio.ToListAsync();
        }

        // GET: api/Extensio/5
        [HttpGet("BuscarNom/{id}")]
        public async Task<ActionResult<Extensio>> GetExtensio(string id)
        {

        }

        // PUT: api/Extensio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("modificarExtensio")]
        public async Task<IActionResult> PutExtensio(Extensio extensio)
        {
            var extensio2 = await _extensioService.PutExtensio(extensio);
            if (extensio2 == null)
            {
                return BadRequest();
            }
            return Ok(extensio2);
        }

        // POST: api/Extensio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Extensio>> PostExtensio(Extensio extensio)
        {
            var extensio2 = await _extensioService.PostExtensio(extensio);
            if (extensio2 == null)
            {
                return BadRequest();
            }
            return Ok(extensio2);
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


    }
}
*/