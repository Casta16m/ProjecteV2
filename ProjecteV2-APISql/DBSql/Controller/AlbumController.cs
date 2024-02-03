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
    /// Controlador de l'album
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly AlbumService _albumService;

        public AlbumController(DataContext context)
        {
            _albumService = new AlbumService(context);
            _context = context;
        }
        /// <summary>
        /// Busca tots els albums
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbum()
        {
            return await _context.Album.ToListAsync();
        }


        /// <summary>
        /// Busca un album per la seva ID_MAC
        /// </summary>
        /// <param name="album"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet("BuscarNom/{album}/{data}")]
        public async Task<ActionResult<IEnumerable<Album>>> GetNomAlbum(string album, DateTime data)
        {
            var albums = await _albumService.GetAlbum(album, data);

            if (albums == null)
            {
                return NotFound();
            }

            return albums;
        }

        /// <summary>
        /// Busca un album per la seva ID_MAC
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        [HttpPut("modificarAlbum")]
        public async Task<IActionResult> PutAlbum(Album album)
        {
            var album2 = await _albumService.PutAlbum(album);
            if (album2 == null)
            {
                return BadRequest();
            }
            return NoContent();
        }

        /// <summary>
        /// Crea un album
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Album>> PostAlbum(Album album)
        {
            var album2 = await _albumService.PostAlbum(album);
            if (album2 == null)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetAlbum", new { id = album.NomAlbum }, album);
        }
        /// <summary>
        /// Elimina un album
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(string id)
        {
            var album = await _albumService.DeleteAlbum(id);
            if (album == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        /// <summary>
        /// Afegeix una canço a un album
        /// </summary>
        /// <param name="NomAlbum"></param>
        /// <param name="data"></param>
        /// <param name="UID"></param>
        /// <returns></returns>
        [HttpPut("AfegirSongAlbum/{NomAlbum}/{data}/{UID}")]
        public async Task<ActionResult<Album>> AfegirSongAlbum(string NomAlbum, DateTime data, string UID)
        {
            var album = await _albumService.AfegirSongAlbum(NomAlbum, data, UID);
            if (album == null)
            {
                return BadRequest();
            }
            else if (album == "No existeix l'album")
            {
                return StatusCode(413);
            }
            else if (album == "No existeix la canço")
            {
                return StatusCode(412);
            }
            return Ok(album);
        }
    }
}
