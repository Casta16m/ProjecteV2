using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoStoreApi.Services;
using ProjecteV2.ApiMongoDB;

namespace ProjecteV2.Controllers;

[Route("MongoApi/v1/[controller]")]
[ApiController]

public class LletresController: ControllerBase
{
    private readonly LletraService _LletresService;

    public LletresController(LletraService lletraService) =>
    _LletresService = lletraService;
    [HttpGet]
    public async Task<ActionResult<List<Lletra>>> GetAllLletres()
    {
        var Lletres = await _LletresService.GetAsync();
        if (Lletres is null)
        {
            return NotFound();
        }
        return Lletres;
    }
    [HttpGet("{_ID}")]
    public async Task<ActionResult<Lletra>> Get(string _ID)
    {
        var Lletres = await _LletresService.GetAsync(_ID);

        if (Lletres is null)
        {
            return NotFound();
        }

        return Lletres;
    }
    [HttpGet("Song/{UIDSong}")]
    public async Task<ActionResult<Lletra>> GetByUIDSong(string UIDSong)
    {
        var Lletres = await _LletresService.GetByUIDSongAsync(UIDSong);

        if (Lletres is null)
        {
            return NotFound();
        }

        return Lletres;
    }
    [HttpPost]
    public async Task<IActionResult> Post(Lletra newLletra)
    {
        IActionResult result;
        try{
            await _LletresService.CreateAsync(newLletra);
            result =  CreatedAtAction(nameof(Get), new { _ID = newLletra._ID }, newLletra);
        } catch (MongoWriteException ex){
            string message = "{\"Category\":\""+ ex.WriteError.Category.ToString()+"\", \"Code\": \""+ex.WriteError.Code.ToString()+"\", \"Message\": \"Duplicate key\"}";
            result = Conflict(message);
        }
        return result;
    }
    [HttpPut("{_ID}")]
    public async Task<IActionResult> Put(string _ID, Lletra updatedLletra)
    {
        IActionResult result;
        var Lletra = await _LletresService.GetAsync(_ID);
        if (Lletra is null)
        {
            result = NotFound();
        }
        else
        {
            await _LletresService.UpdateAsync(_ID, updatedLletra);
            result = NoContent();
        }
        return result;
    }

}