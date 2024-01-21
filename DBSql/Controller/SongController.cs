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
    public class SongController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly SongService _songService;

        public SongController(DataContext context)
        {
            _context = context;
            _songService = new SongService(context);
        }

        // GET: api/Song
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            return await _context.Songs.ToListAsync();
        }


        // GET: api/Song/BuscarNom/5
        [HttpGet("BuscarNom/{nom}")]
        public async Task<ActionResult<IEnumerable<Song>>> GetNomSong(string nom)
        {
            var song = await _songService.GetSong(nom);
            return song;
        }

        [HttpGet("BuscarGenere/{nom}")]
        public async Task<ActionResult<IEnumerable<Song>>> GetGenereSong(string nom)
        {
            var song = await _songService.GetGenere(nom);
            return song;
        }
        // POST: api/Song
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {                

            var song2 = await _songService.PostSong(song.UID, song);
            if (song2 == null)
            {
                return BadRequest();
            }
            return Ok(song2);

        }

        private bool SongExists(string id)
        {
            return _context.Songs.Any(e => e.UID == id);
        }
    }
}
