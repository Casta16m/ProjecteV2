using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoStoreApi.Services;
using ProjecteV2.ApiMongoDB;

namespace ProjecteV2.Controllers;


[Route("FitxersAPI/v1/[controller]")]
[ApiController]
public class SongController : ControllerBase
{
    private readonly SongService _SongService;

    public SongController(SongService SongService) =>
        _SongService = SongService;
        
    [HttpGet]
    public async Task<ActionResult<List<Song>>> GetAll()
    {
        var songs = await _SongService.GetAsync();
        if (songs is null)
        {
            return NotFound();
        }
        return songs;
    }
    [HttpGet("{_ID}")]
    public async Task<ActionResult<Song>> GetSong(string _ID)
    {
        var song = await _SongService.GetAsync(_ID);

        if (song is null)
        {
            return NotFound();
        }

        return song;
    }
    [HttpGet("Audio/{UID}")]
    public async Task<ActionResult<Song>> GetByAudioID(string UID)
    {
        var song = await _SongService.GetByAudioIDAsync(UID);

        if (song is null)
        {
            return NotFound();
        }

        return song;
    }
    [HttpPost]
    public async Task<IActionResult> Post(Song newSong)
    {
        IActionResult result;
        try{
             await _SongService.CreateAsync(newSong);
             result =  CreatedAtAction(nameof(GetSong), new { _ID = newSong._ID }, newSong);
        } catch (MongoWriteException ex){
            string message = "{\"Category\":\""+ ex.WriteError.Category.ToString()+"\", \"Code\": \""+ex.WriteError.Code.ToString()+"\", \"Message\": \"Duplicate key\"}";
           result = Conflict(message);
        }
         return result;
        
    }
    [HttpPut("{_ID}")]
    public async Task<IActionResult> Update(string _ID, Song updatedSong)
    {
        var song = await _SongService.GetAsync(_ID);
        if (song is null)
        {
            return NotFound();
        }
        updatedSong._ID = song._ID;
        await _SongService.UpdateAsync(_ID, updatedSong);
        return NoContent();
    }


}