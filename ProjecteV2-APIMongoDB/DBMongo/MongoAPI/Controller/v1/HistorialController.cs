using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoStoreApi.Services;
using ProjecteV2.ApiMongoDB;

namespace ProjecteV2.Controllers;
/// <summary>
/// Controlador per la col·leccio de historial
/// </summary>

[Route("MongoApi/v1/[controller]")]
[ApiController]

public class HistorialController: ControllerBase
{
    private readonly HistorialService _HistorialService;

    /// <summary>
    /// Constructor de la classe
    /// </summary>
    /// <param name="HistorialService"></param>
    public HistorialController(HistorialService  HistorialService ) =>
        _HistorialService = HistorialService ;
    
    /// <summary>
    /// Aconseguir totes les historials
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Aconseguir una historial específica
    /// </summary>
    /// <param name="_ID"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Aconseguir una historial específica per el seu MAC
    /// </summary>
    /// <param name="MAC"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Aconseguir una historial específica per el seu UIDSong
    /// </summary>
    /// <param name="UIDSong"></param>
    /// <param name="MAC"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// Crear historial
    /// </summary>
    /// <param name="newHistorial"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Actualitzar historial
    /// </summary>
    /// <param name="_ID"></param>
    /// <param name="updatedHistorial"></param>
    /// <returns></returns>
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