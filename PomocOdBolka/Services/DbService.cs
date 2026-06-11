namespace PomocOdBolka.Services;

public class DbService : IDbService
{
    
    private readonly IConfiguration _configuration;

    public DbService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    /*
    public async Task WykonajZapytanieAsync()
    {
        await using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await using var command = new SqlCommand("SELECT 1", connection);

        // command.Parameters.AddWithValue("@Param", wartosc);

        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();

        if (!reader.HasRows) throw new NotFoundException("Brak danych");

        while (await reader.ReadAsync())
        {
            // Mapowanie danych
        }
    }
    */
    
    
    
    
    
    
}