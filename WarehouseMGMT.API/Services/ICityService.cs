using WarehouseMGMT.Domain.Services.Communication;
using WarehouseMGMT.Models;

namespace WarehouseMGMT.Services;

public interface ICityService
{
    Task<IEnumerable<City>> GetAllCitiesAsync();
    Task<City> GetCityByIdAsync(Guid id);
    Task<CityResponse> AddCityAsync(City city);
    Task<CityResponse> UpdateCityAsync(Guid id, City city);
    Task<CityResponse> DeleteCityAsync(Guid id);
}