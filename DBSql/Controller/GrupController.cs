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
    /// <summary>
    /// Controlador del grup
    /// </summary>
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
        /// <summary>
        /// Busca tots els grups
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grup>>> GetGrups()
        {
            return await _context.Grups.ToListAsync();
        }
        /// <summary>
        /// Busca un grup per la seva ID_MAC
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Busca un grup per el seu nom
        /// </summary>
        /// <param name="nom"></param>
        /// <returns></returns>
        [HttpGet("BuscarNom/{nom}")]
        public async Task<ActionResult<IEnumerable<Grup>>> GetNomGrup(string nom)
        {
            var grup = await _grupservice.GetGrup(nom);
            return Ok(grup);
        }
        /// <summary>
        /// Busca un grup per el seu nom i el seu artista
        /// </summary>
        /// <param name="NomGrup"></param>
        /// <param name="NomArtista"></param>
        /// <returns></returns>
        [HttpPut("AfegirArtista/{NomGrup}/{NomArtista}")]
        public async Task<IActionResult> PutAfegirSong(string NomGrup, string NomArtista)
        {
            var resposta = await _grupservice.PutGrup(NomGrup, NomArtista);
            if (resposta == "Okay"){
                return StatusCode(200);
            } 
            else{
                return Ok(resposta);
            }
        }
        /// <summary>
        /// Crea un grup
        /// </summary>
        /// <param name="grup"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Modifica un grup
        /// </summary>
        /// <param name="grup"></param>
        /// <returns></returns>
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

    }
}