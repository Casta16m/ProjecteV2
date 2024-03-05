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
        /// <summary>
        /// Controlador de l'artista
        /// </summary>
        private readonly DataContext _context;
        private readonly ArtistaService _artistaService;

        public ArtistaController(DataContext context)
        {
            _context = context;
            _artistaService = new ArtistaService(context);
        }
        /// <summary>
        /// Busca tots els artistes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artista>>> GetArtistes()
        {
            return await _context.Artistes.ToListAsync();
        }

        /// <summary>
        /// Busca un artista per la seva ID_MAC
        /// </summary>
        /// <param name="nom"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Busca un artista per la seva ID_MAC
        /// </summary>
        /// <param name="artista"></param>
        /// <returns></returns>
        [HttpPut("modificarArtista")]
        public async Task<IActionResult> PutArtista(Artista artista)
        {
            var artista2 = await _artistaService.PutArtista(artista);
            if (artista2 == null)
            {
                return BadRequest();
            }
            return Ok(artista2);
        }

        /// <summary>
        /// Crea un artista
        /// </summary>
        /// <param name="artista"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Elimina un artista
        /// </summary>
        /// <param name="nom"></param>
        /// <returns></returns>
        [HttpDelete("{nom}")]
        public async Task<ActionResult<Artista>> DeleteArtista(string nom)
        {
            var artista = await _artistaService.DeleteArtista(nom);
            if (artista == null)
            {
                return BadRequest();
            }
            return Ok(artista);
        }


    }
}