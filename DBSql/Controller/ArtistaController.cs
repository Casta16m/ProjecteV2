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
    public class ArtistaController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ArtistaService _artistaService;

        public ArtistaController(DataContext context)
        {
            _context = context;
            _artistaService = new ArtistaService(context);
        }

        // GET: api/Artista
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artista>>> GetArtistes()
        {
            return await _context.Artistes.ToListAsync();
        }


        [HttpGet("BuscarNom/{nom}")]
        public async Task<ActionResult<IEnumerable<Artista>>> GetNomArtista(string nom)
        {
            var artista = await _artistaService.GetArtista(nom);

            if (artista == null)
            {
                return NotFound();
            }

            return Ok(artista);
        }


        // GET: api/Artista/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artista>> GetArtista(string id)
        {
            var artista = await _context.Artistes.FindAsync(id);

            if (artista == null)
            {
                return NotFound();
            }

            return artista;
        }

        // PUT: api/Artista/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtista(string id, Artista artista)
        {
            if (id != artista.NomArtista)
            {
                return BadRequest();
            }

            _context.Entry(artista).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistaExists(id))
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

        // POST: api/Artista
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Artista>> PostArtista(Artista artista)
        {
            var crearArtista = await _artistaService.PostArtista(artista);
            if (crearArtista == null)
            {
                return BadRequest();
            }
            return StatusCode(201);
        }


        public bool ArtistaExists(string id)
        {
            return _context.Artistes.Any(e => e.NomArtista == id);
        }
    }
}
