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
    public class AlbumController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly AlbumService _albumService;

        public AlbumController(DataContext context)
        {
            _albumService = new AlbumService(context);
            _context = context;
        }

        // GET: api/Album
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbum()
        {
            return await _context.Album.ToListAsync();
        }



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

        // PUT: api/Album/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/Album
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // DELETE: api/Album/5
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
