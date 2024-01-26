using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjecteV2.ApiSql;
using ProjecteV2.ApiSql.Services;

namespace DBSql.Controller
{
    /// <summary>
    /// Controlador de la canço
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly SongService _songService;

        public SongController(DataContext context)
        {
            _context = context;
            _songService = new SongService(context);
        }
        /// <summary>
        /// Busca totes les cançons
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            return await _context.Songs.ToListAsync();
        }
        /// <summary>
        /// Busca una canço per la seva ID_MAC
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        [HttpGet("BuscarUID/{UID}")]
        public async Task<ActionResult<Song>> GetSong(string UID)
        {
            var song = await _songService.GetSongEspecifica(UID);
            if (song == null)
            {
                return NotFound();
            }
            return Ok(song);
        }
        /// <summary>
        /// Busca una canço per el seu nom
        /// </summary>
        /// <param name="nom"></param>
        /// <returns></returns>
        [HttpGet("BuscarNom/{nom}")]
        public async Task<List<Song>> GetNomSong(string nom)
        {
            var song = await _songService.GetSong(nom);
            return song;
        }
        /// <summary>
        /// Busca una canço per el seu genere
        /// </summary>
        /// <param name="nom"></param>
        /// <returns></returns>
        [HttpGet("BuscarGenere/{nom}")]
        public async Task<ActionResult<IEnumerable<Song>>> GetGenereSong(string nom)
        {
            var song = await _songService.GetGenere(nom);
            return song;
        }
        /// <summary>
        /// Busca una canço per el seu artista
        /// </summary>
        /// <param name="song"></param>
        /// <param name="NomExtensio"></param>
        /// <returns></returns>
        [HttpPost("crearSong/{NomExtensio}")]
        public async Task<ActionResult<Song>> PostSong(Song song, string NomExtensio)
        {                
            var song2 = await _songService.PostSong(song, NomExtensio);
            if (song2 == null)
            {
                return BadRequest();
            }
            return Ok(song2);
        }
        /// <summary>
        /// Modifica una canço
        /// </summary>
        /// <param name="song"></param>
        /// <returns></returns>
        [HttpPut("modificarSong")]
        public async Task<ActionResult<Song>> PutSong(Song song)
        {
            var song2 = await _songService.PutSong(song);
            if (song2 == null)
            {
                return StatusCode(412);
            }
            return Ok(song2);

        }
        /// <summary>
        /// Elimina una canço
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        [HttpDelete("{UID}")]
        public async Task<ActionResult<Song>> DeleteSong(string UID)
        {
            var song = await _songService.DeleteSong(UID);
            if (song == null)
            {
                return NotFound();
            }
            return Ok(song);
        }

    }
}