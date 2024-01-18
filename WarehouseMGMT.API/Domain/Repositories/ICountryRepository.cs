using WarehouseMGMT.Models;

namespace WarehouseMGMT.Domain.Repositories;

public interface ICountryRepository
{
    Task<IEnumerable<Country>> GetAllCountriesAsync();
    Task<Country?> GetCountryByIdAsync(Guid id);
    Task<Country?> GetCountryByNameAsync(string name);
    Task AddCountryAsync(Country country);
    string GetCountryNameAsync(Warehouse warehouse);
    void Update(Country country);
    void Remove(Country country);
}