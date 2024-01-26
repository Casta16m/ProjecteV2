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
    /// Controlador de la participa
    /// </summary>
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
    /// <summary>
    /// Busca totes les participacions
    /// </summary>
    /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participa>>> GetParticipa()
        {
            return await _context.Participa.ToListAsync();
        }
    /// <summary>
    /// Busca una participacio per la seva ID_MAC
    /// </summary>
    /// <param name="UID"></param>
    /// <returns></returns>
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
        /// <summary>
        /// Busca una participacio per la seva ID_MAC
        /// </summary>
        /// <param name="participa"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Crea una participacio
        /// </summary>
        /// <param name="participa"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Elimina una participacio
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
