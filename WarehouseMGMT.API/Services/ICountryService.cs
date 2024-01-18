using WarehouseMGMT.Domain.Services.Communication;
using WarehouseMGMT.Models;

namespace WarehouseMGMT.Services;

public interface ICountryService
{
    Task<IEnumerable<Country>> GetAllCountriesAsync();
    Task<Country?> GetCountryByIdAsync(Guid id);
    Task<Country?> GetCountryByNameAsync(string name);
    Task<CountryResponse> AddCountryAsync(Country country);
    Task<CountryResponse> UpdateCountryAsync(Guid id, Country country);
    Task<CountryResponse> DeleteCountryAsync(Guid id);
    string GetCountryNameAsync(Warehouse warehouse);

}