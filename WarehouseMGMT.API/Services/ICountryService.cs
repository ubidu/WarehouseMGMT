using WarehouseMGMT.Domain.Services.Communication;
using WarehouseMGMT.Models;

namespace WarehouseMGMT.Services;

public interface ICountryService
{
    Task<IEnumerable<Country>> GetAllCountriesAsync();
    Task<CountryResponse> AddCountryAsync(Country country);
    Task<CountryResponse> UpdateCountryAsync(Guid id, Country country);
    Task<CountryResponse> DeleteCountryAsync(Guid id);

}