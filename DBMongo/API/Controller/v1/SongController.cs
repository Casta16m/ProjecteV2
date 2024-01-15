using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoStoreApi.Services;
using ProjecteV2.ApiMongoDB;

namespace ProjecteV2.Controllers;


[Route("MongoApi/v1/[controller]")]
[ApiController]
public class SongController : ControllerBase
{
    private readonly SongService _SongService;

    public SongController(SongService SongService) =>
        _SongService = SongService;

    [HttpGet("{_ID}")]
    public async Task<ActionResult<Song>> Get(string _ID)
    {
        var song = await _SongService.GetAsync(_ID);

        if (song is null)
        {
            return NotFound();
        }

        return song;
    }
    [HttpPost]
    public async Task<IActionResult> Post(Song newSong)
    {
        try
        {
            await _SongService.CreateAsync(newSong);
            Console.WriteLine("Song created");
            return CreatedAtAction(nameof(Get), new { _ID = newSong._ID }, newSong);
         
        }
        catch (Exception ex)
        {
            // Aquí puedes manejar la excepción como quieras. Por ejemplo, podrías devolver un 500 Internal Server Error.
            return StatusCode(500, ex.Message);
        }
    }
    [HttpPut("{_ID}")]
    public async Task<IActionResult> Update(string _ID, Song updatedSong)
    {
        var song = await _SongService.GetAsync(_ID);
        if (song is null)
        {
            return NotFound();
        }
        await _SongService.UpdateAsync(_ID, updatedSong);
        return NoContent();
    }
    [HttpDelete("{_ID}")]
    public async Task<IActionResult> Delete(string _ID)
    {
        var song = await _SongService.GetAsync(_ID);
        if (song is null)
        {
            return NotFound();
        }
        await _SongService.RemoveAsync(song._ID);
        return NoContent();
    }
    //Buscan per OID rertorna audioID


}