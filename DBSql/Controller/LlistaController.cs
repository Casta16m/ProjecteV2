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
        [HttpGet("BuscarNom/{Nom}")]
        public async Task<ActionResult<Llista>> GetLlista(string Nom)
        {
            LlistaService LlistaService = new LlistaService();
            var llista = await LlistaService.GetLlista(Nom, _context);
            if (llista == null)
            {
                return NotFound();
            }
            return Ok(llista);
        }
        [HttpGet("BuscarMac/{ID_MAC}")]
        public async Task<ActionResult<Llista>> GetLlistaMAC(string ID_MAC)
        {

            LlistaService LlistaService = new LlistaService();
            var llistaMac = await LlistaService.GetLlistaMac(ID_MAC, _context);
            if (llistaMac == null)
            {
                return NotFound();
            }
            return Ok(llistaMac);
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
            LlistaService LlistaService = new LlistaService();
            var llista2 = await LlistaService.PostLlista(llista, _context);
            if (llista2 == null)
            {
                return BadRequest();
            }
            return Ok(llista2);
        }
        
        //----------------------------------------------------------------------------------------

    [HttpPut("AfegirSong/{NomLlista}/{UID}/{ID_MAC}")]
    public async Task<IActionResult> PutLlistaSong(string NomLlista, string UID, string ID_MAC)
    {
        var llista = await _context.Llista.Include(a => a.songs).FirstOrDefaultAsync(a => a.Nom == NomLlista);
        
        if (llista == null)
        {
            return StatusCode(401);
        }

        var ID_MAC1 = await _context.Llista.FirstOrDefaultAsync(a => a.ID_MAC == ID_MAC);
        if (ID_MAC1 == null)
        {
            return StatusCode(403);
        }

        var song = await _context.Songs.FirstOrDefaultAsync(a => a.UID == UID);
        if (song == null)
        {
            return StatusCode(402);
        }



        if(llista.ID_MAC == ID_MAC1.ID_MAC)
        {
            llista.songs.Add(song);
            _context.Entry(llista).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return StatusCode(200);

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
