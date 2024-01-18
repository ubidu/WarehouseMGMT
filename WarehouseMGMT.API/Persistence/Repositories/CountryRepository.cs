using Microsoft.EntityFrameworkCore;
using WarehouseMGMT.Domain.Repositories;
using WarehouseMGMT.Models;

namespace WarehouseMGMT.Persistence.Repository;

public class CountryRepository : BaseRepository, ICountryRepository
{
    public CountryRepository(WarehouseMGMTDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Country>> GetAllCountriesAsync()
    {
        return await _context.Countries.ToListAsync();
    }

    public async Task<Country?> GetCountryByIdAsync(Guid id)
    {
        return await _context.Countries.FindAsync(id); 
    }

    public async Task AddCountryAsync(Country country)
    {
        await _context.Countries.AddAsync(country);
    }

    public void Update(Country country)
    {
        _context.Countries.Update(country);
    }

    public void Remove(Country country)
    {
        _context.Countries.Remove(country);
    }
}