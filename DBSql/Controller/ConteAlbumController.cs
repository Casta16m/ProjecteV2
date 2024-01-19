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
    public class ConteAlbumController : ControllerBase
    {
        private readonly DataContext _context;

        public ConteAlbumController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ConteAlbum
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConteAlbum>>> GetConteAlbum()
        {
            return await _context.ConteAlbum.ToListAsync();
        }

        // GET: api/ConteAlbum/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConteAlbum>> GetConteAlbum(string id)
        {
            var conteAlbum = await _context.ConteAlbum.FindAsync(id);

            if (conteAlbum == null)
            {
                return NotFound();
            }

            return conteAlbum;
        }

        // PUT: api/ConteAlbum/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConteAlbum(string id, ConteAlbum conteAlbum)
        {
            if (id != conteAlbum.NomSong)
            {
                return BadRequest();
            }

            _context.Entry(conteAlbum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConteAlbumExists(id))
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

        // POST: api/ConteAlbum
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConteAlbum>> PostConteAlbum(ConteAlbum conteAlbum)
        {
            _context.ConteAlbum.Add(conteAlbum);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ConteAlbumExists(conteAlbum.NomSong))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetConteAlbum", new { id = conteAlbum.NomSong }, conteAlbum);
        }

        // DELETE: api/ConteAlbum/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConteAlbum(string id)
        {
            var conteAlbum = await _context.ConteAlbum.FindAsync(id);
            if (conteAlbum == null)
            {
                return NotFound();
            }

            _context.ConteAlbum.Remove(conteAlbum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConteAlbumExists(string id)
        {
            return _context.ConteAlbum.Any(e => e.NomSong == id);
        }
    }
}
