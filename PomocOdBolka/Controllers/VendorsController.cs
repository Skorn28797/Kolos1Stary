using Microsoft.AspNetCore.Mvc;
using PomocOdBolka.Services;
using PomocOdBolka.Exceptions;

namespace PomocOdBolka.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VendorsController : ControllerBase
{
    private readonly IDbService _dbService;
    
    public VendorsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetVendor(string code)
    {
        try
        {
            // var result = await _dbService.MetodaAsync();
            var result = await _dbService.GetVendorDataAsync(code);
            return Ok(); //rezultat
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message); 
        }
    }
    
    
    
}