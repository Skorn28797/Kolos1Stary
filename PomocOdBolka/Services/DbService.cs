using PomocOdBolka.DTOs;
using PomocOdBolka.Exceptions;
using System.Data.SqlClient;

namespace PomocOdBolka.Services;

public class DbService : IDbService
{
    
    private readonly IConfiguration _configuration;

    public DbService(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public async Task<GetVendorResponse> GetVendorDataAsync(string vendorCode)
    {
        await using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));


        var query = @"
            SELECT
	            v.Code, v.Name AS VendorName,
	            p.Id AS ProductId, p.Name AS ProductName, p.Description, p.StickerPrice,
	            pt.Id AS TypeId, pt.Name AS TypeName,
	            m.Id AS MakerId, m.Name AS MakerName,
	            vp.Amount, vp.PricePerUnit
            FROM Vendors v
            LEFT JOIN VendorProducts vp ON v.Code = vp.VendorCode
            LEFT JOIN Products p ON vp.ProductId = p.Id
            LEFT JOIN ProductTypes pt ON p.ProductTypeId = pt.Id
            LEFT JOIN Makers m ON p.MakerId = m.Id
            WHERE v.Code = @Code;";

        await using var command = new SqlCommand(query, connection);
        
        command.Parameters.AddWithValue("@Code", vendorCode);

        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();

        if (!reader.HasRows)
            throw new NotFoundException($"Dostawca o kodzie {vendorCode} nie istnieje.");

        GetVendorResponse response = null!;

        while (await reader.ReadAsync())
        {
            if (response == null)
            {
                response = new GetVendorResponse
                {
                    Code = reader["Code"].ToString(),
                    Name = reader["VendorName"].ToString()
                };
            }

            if (!reader.IsDBNull(reader.GetOrdinal("ProductId")))
            {
                response.Products.Add(new ProductDto
                {
                    Id = (int)reader["ProductId"],
                    Name = reader["ProductName"].ToString(),
                    
                    //nullowane
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader["Description"].ToString(),
                    StrickerPrice = (decimal)reader["StickerPrice"],
                    ProductType = new ProductTypeDto
                    {
                        Id = (int)reader["TypeId"],
                        Name = reader["TypeName"].ToString()
                    },
                    Maker = new MakerDto
                    {
                        Id = (int)reader["MakerId"],
                        Name = reader["MakerName"].ToString()
                    },
                    VendorOffer = new VendorOfferDto
                    {
                        Amount = (int)reader["Amount"],
                        PricePerUnit = (decimal)reader["PricePerUnit"]
                    }
                    
                });
            }
        }

        return response;

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