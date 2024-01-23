using System;
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
    public class ParticipaController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ParticipaService _participaService;

        public ParticipaController(DataContext context)
        {
            _context = context;
            _participaService = new ParticipaService(context);
        }

        // GET: api/Participa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participa>>> GetParticipa()
        {
            return await _context.Participa.ToListAsync();
        }

        // GET: api/Participa/5
        [HttpGet("BuscarSong/{UID}")]
        public async Task<ActionResult<Participa>> GetParticipa(string UID)
        {
            var participa = await _participaService.GetParticipa(UID);

            if (participa == null)
            {
                return NotFound();
            }

            return Ok(participa);
        }

        // PUT: api/Participa/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutParticipa(Participa participa)
        {
            var participa2 = await _participaService.PutParticipaGeneral(participa);
            if (participa2 == null)
            {
                return NotFound();
            }
            return Ok(participa2);
        }

        // POST: api/Participa
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Participa>> PostParticipa(Participa participa)
        {
            var participa2 = await _participaService.PostParticipa(participa);
            if (participa2 == null)
            {
                return BadRequest();
            }
            return Ok(participa2);

        }

        // DELETE: api/Participa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipa(string id)
        {
            var participa = await _participaService.DeleteParticipa(id);
            if (participa == null)
            {
                return NotFound();
            }
            return Ok(participa);
        }
    }
}
