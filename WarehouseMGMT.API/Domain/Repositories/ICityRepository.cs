using WarehouseMGMT.Models;

namespace WarehouseMGMT.Domain.Repositories;

public interface ICityRepository
{
    Task<IEnumerable<City>> GetAllCitiesAsync();
    Task<IEnumerable<City>> GetCitiesByCountryIdAsync(Guid countryId);
    Task<City?> GetCityByIdAsync(Guid id);
    Task<City?> GetCityByNameAsync(string name);
    Task<City?> AddCityAsync(City city);
    Task<City?> UpdateCityAsync(City city);
    Task<City?> DeleteCityAsync(Guid id);
    Task<City?> DeleteCityAsync(City city);
    Task<City?> DeleteCityAsync(string name);
    string GetCityNameAsync(Warehouse warehouse);
}