using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoStoreApi.Services;
using ProjecteV2.ApiMongoDB;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.IO;
using System.Threading.Tasks;

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

    [HttpGet("GetAudio/{UID}")]
    public async Task<IActionResult> GetAudio(string UID)
    {
        try
        {
            var song = await _SongService.GetByAudioIDAsync(UID);

            if (song == null)
            {
                return NotFound("No existe la canción");
            }

            var audioStream = await _SongService.GetAudioStreamAsync(song.AudioFileId);

            if (audioStream == null)
            {
                return Conflict("No se ha podido recuperar el archivo de audio");
            }

            return File(audioStream, "audio/mp3", $"audio_{UID}.mp3");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Error interno del servidor.", Error = ex.Message });
        }
    }


    [HttpPost]
    public async Task<IActionResult> UploadSong([FromForm] SongUploadModel songModel)
    {
        try
        {
            var song = new Song
            {
                UID = songModel.Uid
            };

            // Subir el archivo de audio a GridFS
            var uploadOptions = new GridFSUploadOptions
            {
                Metadata = new BsonDocument("contentType", songModel.Audio.ContentType)
            };

            using (var stream = songModel.Audio.OpenReadStream())
            {
                var fileId = await _SongService.UploadAudioAsync(songModel.Audio.FileName, stream, uploadOptions);
                song.AudioFileId = fileId;
            }

            await _SongService.CreateAsync(song);

            return Ok(new { Message = "Canción y archivo de audio subidos con éxito." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Error interno del servidor.", Error = ex.Message });
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
        updatedSong._ID = song._ID;
        await _SongService.UpdateAsync(_ID, updatedSong);
        return NoContent();
    }


}