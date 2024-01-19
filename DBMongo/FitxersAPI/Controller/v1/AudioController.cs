using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoStoreApi.Services;
using ProjecteV2.ApiMongoDB;

namespace ProjecteV2.Controllers;

[Route("FitxerAPI/v1/[controller]")]
[ApiController]
public class AudioController: ControllerBase
{
    public readonly AudioService _AudioService; 
    public AudioController(AudioService AudioService) =>
        _AudioService = AudioService;
        
    [HttpGet]
    public async Task<ActionResult<List<Audio>>> GetAll()
    {
        var Audio = await _AudioService.GetAsync();
        if (Audio is null)
        {
            return NotFound();
        }
        return Audio;
    }
    [HttpGet("{_ID}")]
    public async Task<ActionResult<Audio>> GetAudio(string _ID)
    {
        var Audio = await _AudioService.GetAsync(_ID);

        if (Audio is null)
        {
            return NotFound();
        }

        return Audio;
    }
    [HttpPost]
    public async Task<IActionResult> Post(Audio newAudio)
    {
        IActionResult result;
        try{
             await _AudioService.CreateAsync(newAudio);
             result =  CreatedAtAction(nameof(GetAudio), new { _ID = newAudio._ID }, newAudio);
        } catch (MongoWriteException ex){
            string message = "{\"Category\":\""+ ex.WriteError.Category.ToString()+"\", \"Code\": \""+ex.WriteError.Code.ToString()+"\", \"Message\": \"Duplicate key\"}";
            result = Conflict(message);
        }
        return result;
    }
    [HttpPut("{_ID}")]
    public async Task<IActionResult> Put(string _ID, Audio updatedAudio)
    {
        var Audio = await _AudioService.GetAsync(_ID);

        if (Audio is null)
        {
            return NotFound();
        }

        await _AudioService.UpdateAsync(_ID, updatedAudio);

        return NoContent();
    }
    
}