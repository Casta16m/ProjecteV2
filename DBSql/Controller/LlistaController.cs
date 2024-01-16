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
    public class LlistaController : ControllerBase
    {
        private readonly DataContext _context;

        public LlistaController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Llista
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Llista>>> GetLlista()
        {
            return await _context.Llista.ToListAsync();
        }

        // GET: api/Llista/5
        [HttpGet("getLlistaName/{Nom}")]
        public async Task<ActionResult<Llista>> GetLlista(string Nom)
        {
            var Lista = await _context.Llista.Include(a => a.songs).FirstOrDefaultAsync(a => a.Nom == Nom);
            //var llista = await _context.Llista.FindAsync(Nom);

            if (Lista == null)
            {
                return NotFound();
            }

            return Lista;
        }

        // PUT: api/Llista/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLlista(string id, Llista llista)
        {
            if (id != llista.Nom)
            {
                return BadRequest();
            }

            _context.Entry(llista).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LlistaExists(id))
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

        // POST: api/Llista
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Llista>> PostLlista(Llista llista)
        {
            _context.Llista.Add(llista);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LlistaExists(llista.Nom))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLlista", new { id = llista.Nom }, llista);
        }
        //----------------------------------------------------------------------------------------
        [HttpPut("AfegirSong/{id}")]
        public async Task<IActionResult> PutLlista(string id, Song Song)
        {
            var llista = await _context.Llista.FindAsync(id);
            if (llista == null)
            {
                return NotFound();
            }
            llista.songs.Add(Song);
            _context.Entry(llista).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LlistaExists(id))
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

        //----------------------------------------------------------------------------------------

    [HttpPut("AfegirSong/{NomLlista}/{UID}/{ID_MAC}")]
    public async Task<IActionResult> PutLlistaSong(string NomLlista, string UID, string ID_MAC)
    {
        var llista = await _context.Llista.Include(a => a.songs).FirstOrDefaultAsync(a => a.Nom == NomLlista);
        
        if (llista == null)
        {
            return NotFound();
        }

        var song = await _context.Songs.FirstOrDefaultAsync(a => a.UID == UID);
        if (song == null)
        {
            return NotFound();
        }

        var ID_MAC1 = await _context.Llista.FirstOrDefaultAsync(a => a.ID_MAC == ID_MAC);
        if (ID_MAC1 == null)
        {
            return NotFound();
        }

        if(llista.ID_MAC == ID_MAC1.ID_MAC)
        {
            llista.songs.Add(song);
            _context.Entry(llista).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return StatusCode(400);

        }
        else
        {
            return NotFound();
        }




    } 
        // DELETE: api/Llista/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLlista(string id)
        {
            var llista = await _context.Llista.FindAsync(id);
            if (llista == null)
            {
                return NotFound();
            }

            _context.Llista.Remove(llista);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LlistaExists(string id)
        {
            return _context.Llista.Any(e => e.Nom == id);
        }
    }
}
