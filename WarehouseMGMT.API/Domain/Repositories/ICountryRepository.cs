using WarehouseMGMT.Models;

namespace WarehouseMGMT.Domain.Repositories;

public interface ICountryRepository
{
    Task<IEnumerable<Country>> GetAllCountriesAsync();
    Task<Country?> GetCountryByIdAsync(Guid id);
    Task AddCountryAsync(Country country);
    void Update(Country country);
    void Remove(Country country);
}