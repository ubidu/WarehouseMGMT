using WarehouseMGMT.Models;

namespace WarehouseMGMT.Domain.Repositories;

public interface ICityRepository
{
    Task<IEnumerable<City>> GetAllCitiesAsync();
    Task<City?> GetCityByIdAsync(Guid id);
    Task AddCityAsync(City city);
    void Update(City city);
    void Remove(City city);

}