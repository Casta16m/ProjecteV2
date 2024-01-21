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
        private readonly LlistaService _llistaService;

        public LlistaController(DataContext context)
        {
            _context = context;
            _llistaService = new LlistaService(context);
        }

        // GET: api/Llista
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Llista>>> GetLlista()
        {
            return await _context.Llista.ToListAsync();
        }

        // GET: api/Llista/5
        [HttpGet("BuscarNom/{Nom}")]
        public async Task<ActionResult<Llista>> GetLlista(string Nom)
        {
            var llista = await _llistaService.GetLlista(Nom, _context);
        
            if (llista == null)
            {
                return NotFound();
            }
            return Ok(llista);
        }
        [HttpGet("BuscarMac/{ID_MAC}")]
        public async Task<ActionResult<Llista>> GetLlistaMAC(string ID_MAC)
        {

            var llistaMac = await _llistaService.GetLlistaMac(ID_MAC, _context);
            if (llistaMac == null)
            {
                return NotFound();
            }
            return Ok(llistaMac);
        }

        // POST: api/Llista
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Llista>> PostLlista(Llista llista)
        {
            var llista2 = await _llistaService.PostLlista(llista, _context);
            if (llista2 == null)
            {
                return BadRequest();
            }
            return Ok(llista2);
        }
        
        //----------------------------------------------------------------------------------------

    [HttpPut("AfegirSong/{NomLlista}/{UID}/{ID_MAC}")]
    public async Task<IActionResult> PutLlistaSong(string NomLlista, string UID, string ID_MAC)
    {

        var llista = await _llistaService.PutLlista(NomLlista, UID, ID_MAC);
        if (llista == null)
        {
            return NotFound();
        }
        return StatusCode(200);
    } 
        private bool LlistaExists(string id)
        {
            return _context.Llista.Any(e => e.Nom == id);
        }
    }
}
