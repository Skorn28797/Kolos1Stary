using PomocOdBolka.DTOs;

namespace PomocOdBolka.Services;

public interface IDbService
{
    Task<GetVendorResponse> GetVendorDataAsync(string vendorCode);
}

