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
    public class InstrumentController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly InstrumentService _instrumentService;

        public InstrumentController(DataContext context)
        {
            _context = context;
            _instrumentService = new InstrumentService(context);
        }

        // GET: api/Instrument
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instrument>>> GetInstrument()
        {
            return await _context.Instrument.ToListAsync();
        }

        // GET: api/Instrument/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Instrument>> GetInstrument(string id)
        {
            var instrument = await _context.Instrument.FindAsync(id);

            if (instrument == null)
            {
                return NotFound();
            }

            return instrument;
        }


        [HttpGet("BuscarNom/{nom}")]
        public async Task<ActionResult<Instrument>> GetNomInstrument(string nom)
        {
            var instrument = await _instrumentService.GetInstrument(nom);

            if (instrument == null)
            {
                return NotFound();
            }

            return Ok(instrument);
        }


        // PUT: api/Instrument/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstrument(string id, Instrument instrument)
        {
            if (id != instrument.Nom)
            {
                return BadRequest();
            }

            _context.Entry(instrument).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstrumentExists(id))
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

        // POST: api/Instrument
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Instrument>> PostInstrument(Instrument instrument)
        {
            var crearInstrument = await _instrumentService.PostInstrument(instrument);
            if (crearInstrument == null)
            {
                return BadRequest();
            }
            return StatusCode(201);
        }

        // DELETE: api/Instrument/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstrument(string id)
        {
            var instrument = await _context.Instrument.FindAsync(id);
            if (instrument == null)
            {
                return NotFound();
            }

            _context.Instrument.Remove(instrument);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public bool InstrumentExists(string id)
        {
            return _context.Instrument.Any(e => e.Nom == id);
        }
    }
}
