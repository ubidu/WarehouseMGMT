using Microsoft.EntityFrameworkCore;
using WarehouseMGMT.Domain.Repositories;
using WarehouseMGMT.Models;

namespace WarehouseMGMT.Persistence.Repository;

public class CityRepository : BaseRepository, ICityRepository
{
    public CityRepository(WarehouseMGMTDbContext context) : base(context)
    {
        
    }

    public async Task<IEnumerable<City>> GetAllCitiesAsync()
    {
        return await _context.Cities.ToListAsync();
    }

    public Task<IEnumerable<City>> GetCitiesByCountryIdAsync(Guid countryId)
    {
        throw new NotImplementedException();
    }

    public Task<City?> GetCityByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<City?> GetCityByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task<City?> AddCityAsync(City city)
    {
        throw new NotImplementedException();
    }

    public Task<City?> UpdateCityAsync(City city)
    {
        throw new NotImplementedException();
    }

    public Task<City?> DeleteCityAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<City?> DeleteCityAsync(City city)
    {
        throw new NotImplementedException();
    }

    public Task<City?> DeleteCityAsync(string name)
    {
        throw new NotImplementedException();
    }

    public string GetCityNameAsync(Warehouse warehouse)
    {
        throw new NotImplementedException();
    }
}