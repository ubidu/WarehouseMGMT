using Microsoft.EntityFrameworkCore;
using WarehouseMGMT.Domain.Repositories;
using WarehouseMGMT.Models;

namespace WarehouseMGMT.Persistence.Repository;

public class WarehouseRepository : BaseRepository, IWarehouseRepository
{
    public WarehouseRepository(WarehouseMGMTDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Warehouse>> GetAllWarehousesAsync()
    {
        return await _context.Warehouses.ToListAsync();
    }

    public async Task<Warehouse> GetWarehouseByIdAsync(Guid id)
    {
        return await _context.Warehouses.FindAsync(id);
    }

    public async Task AddWarehouseAsync(Warehouse warehouse)
    {
        await _context.Warehouses.AddAsync(warehouse);
    }

    public void Update(Warehouse warehouse)
    {
        _context.Warehouses.Update(warehouse);
    }

    public void Remove(Warehouse warehouse)
    {
        _context.Warehouses.Remove(warehouse);
    }
}