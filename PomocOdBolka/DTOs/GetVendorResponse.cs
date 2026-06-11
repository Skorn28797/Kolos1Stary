namespace PomocOdBolka.DTOs;

public class GetVendorResponse
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public List<ProductDto> Products { get; set; } = new();
}