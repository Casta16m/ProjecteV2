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
    public class GrupController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly GrupService _grupservice;

        public GrupController(DataContext context)
        {
            _context = context;
            _grupservice = new GrupService(context);
        }

        // GET: api/Grup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grup>>> GetGrups()
        {
            return await _context.Grups.ToListAsync();
        }

        // GET: api/Grup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Grup>> GetGrup(string id)
        {
            var grup = await _context.Grups.FindAsync(id);

            if (grup == null)
            {
                return NotFound();
            }

            return grup;
        }
        [HttpGet("BuscarNom/{nom}")]
        public async Task<ActionResult<IEnumerable<Grup>>> GetNomGrup(string nom)
        {
            
            var grup = await _context.Grups.Include(a=> a.artistes).Where(a => a.NomGrup.Contains(nom)).ToListAsync();

            if (grup == null)
            {
                return NotFound();
            }

            return grup;
        }
        [HttpPut("AfegirArtista/{NomGrup}/{NomArtista}")]
        public async Task<IActionResult> PutAfegirSong(string NomGrup, string NomArtista)
        {
            var resposta = await _grupservice.PutGrup(NomGrup, NomArtista);
            if (resposta == "Okay"){
                return StatusCode(200);
            } 
            else{
                return StatusCode(400);
            }
        }
        // POST: api/Grup
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Grup>> PostGrup(Grup grup)
        {
            var postGrup = await _grupservice.PostGrup(grup);
            if (postGrup == null)
            {
                return BadRequest();
            }
            return Ok(postGrup);
        }
        [HttpPut("modificarGrup")]
        public async Task<Grup> PutGrup(Grup grup)
        {
            var grup2 = await _grupservice.ModificarTotGrup(grup);
            if (grup2 == null)
            {
                return null;
            }
            return grup2;
        }
        public bool GrupExists(string id)
        {
            return _context.Grups.Any(e => e.NomGrup == id);
        }
    }
}