using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<Historial>> Get(string _ID)
    {
        var Historial = await _HistorialService.GetAsync(_ID);

        if (Historial is null)
        {
            return NotFound();
        }

        return Historial;
    }
   

}