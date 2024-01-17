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
    
    [HttpGet("{_ID}")]
    public async Task<ActionResult<Historial>> GetHistorial(string _ID)
    {
        var Historial = await _HistorialService.GetAsyncHistorial(_ID);

        if (Historial is null)
        {
            return NotFound();
        }

        return Historial;
    }
    [HttpPost]
    public async Task<IActionResult> PostHistorial(Historial newHistorial)
    {
        IActionResult result;
        try{
            await _HistorialService.CreateAsyncHistorial(newHistorial);
            result =  CreatedAtAction(nameof(GetHistorial), new { _ID = newHistorial._ID }, newHistorial);
        } catch (MongoWriteException ex){
            string message = "{\"Category\":\""+ ex.WriteError.Category.ToString()+"\", \"Code\": \""+ex.WriteError.Code.ToString()+"\", \"Message\": \"Duplicate key\"}";
            result = Conflict(message);
        }
        return result;
    }
   

}