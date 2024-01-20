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

    public async Task<City?> GetCityByIdAsync(Guid id)
    {
        return await _context.Cities.FindAsync(id);
    }

    public async Task AddCityAsync(City city)
    {
        await _context.Cities.AddAsync(city);
    }

    public void Update(City city)
    {
        _context.Cities.Update(city);
    }

    public void Remove(City city)
    {
        _context.Cities.Remove(city);
    }
}