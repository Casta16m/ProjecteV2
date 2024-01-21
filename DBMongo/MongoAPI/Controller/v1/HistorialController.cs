using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoStoreApi.Services;
using ProjecteV2.ApiMongoDB;

namespace ProjecteV2.Controllers;

[Route("MongoApi/v1/[controller]")]
[ApiController]

public class HistorialController: ControllerBase
{
    private readonly HistorialService _HistorialService;

    public HistorialController(HistorialService  HistorialService ) =>
        _HistorialService = HistorialService ;
    [HttpGet]
    public async Task<ActionResult<List<Historial>>> GetAllHistorial()
    {
        var Historial = await _HistorialService.GetAsync();
        if (Historial is null)
        {
            return NotFound();
        }
        return Historial;
    }
    [HttpGet("{_ID}")]
    public async Task<ActionResult<Historial>> Get(string _ID)
    {
        var Historial = await _HistorialService.GetAsync(_ID);

        if (Historial is null)
        {
            return NotFound();
        }

        return Historial;
    }
    [HttpGet("Mac/{MAC}")]
    public async Task<ActionResult<Historial>> GetByMAC(string MAC)
    {
        var Historial = await _HistorialService.GetByMACAsync(MAC);

        if (Historial is null)
        {
            return NotFound();
        }

        return Historial;
    }
    [HttpGet("Song/MAC/{UIDSong}/{MAC}")]
    public async Task<ActionResult<Historial>> GetByUIDSongAndMAC(string UIDSong, string MAC)
    {
        var Historial = await _HistorialService.GetByUIDSongAndMACAsync(UIDSong, MAC);

        if (Historial is null)
        {
            return NotFound();
        }

        return Historial;
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(Historial newHistorial)
    {
        IActionResult result;
        try{
            await _HistorialService.CreateAsync(newHistorial);
            result =  CreatedAtAction(nameof(Get), new { _ID = newHistorial._ID }, newHistorial);
        } catch (MongoWriteException ex){
            string message = "{\"Category\":\""+ ex.WriteError.Category.ToString()+"\", \"Code\": \""+ex.WriteError.Code.ToString()+"\", \"Message\": \"Duplicate key\"}";
            result = Conflict(message);
        }
        return result;
    }
    [HttpPut("{_ID}")]
    public async Task<IActionResult> Update(string _ID, Historial updatedHistorial)
    {
        var historial = await _HistorialService.GetAsync(_ID);
        if (historial is null)
        {
            return NotFound();
        }
        updatedHistorial._ID = historial._ID;
        await _HistorialService.UpdateAsync(_ID, updatedHistorial);
        return NoContent();
    }
   

}