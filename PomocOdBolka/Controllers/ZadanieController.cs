using Microsoft.AspNetCore.Mvc;
using PomocOdBolka.Services;
using PomocOdBolka.Exceptions;

namespace PomocOdBolka.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ZadanieController : ControllerBase
{
    private readonly IDbService _dbService;
    
    public ZadanieController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            // var result = await _dbService.MetodaAsync();
            return Ok(); //rezultat
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message); 
        }
    }
    
    
    
}