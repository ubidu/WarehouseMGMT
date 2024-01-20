using Microsoft.EntityFrameworkCore;
using WarehouseMGMT.Domain.Repositories;
using WarehouseMGMT.Models;

namespace WarehouseMGMT.Persistence.Repository;

public class WarehouseContentRepository : BaseRepository, IWarehouseContentRepository
{
    public WarehouseContentRepository(WarehouseMGMTDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<WarehouseContent>> GetAllWarehouseContentsAsync()
    {
        return await _context.WarehouseContents.ToListAsync();
    }

    public async Task<WarehouseContent> GetWarehouseContentByIdAsync(Guid id)
    {
        return await _context.WarehouseContents.FindAsync(id);
    }

    public async Task AddWarehouseContentAsync(WarehouseContent warehouseContent)
    {
        await _context.WarehouseContents.AddAsync(warehouseContent);
    }

    public void Update(WarehouseContent warehouseContent)
    {
        _context.WarehouseContents.Update(warehouseContent);
    }

    public void Remove(WarehouseContent warehouseContent)
    {
        _context.WarehouseContents.Remove(warehouseContent);
    }
    
}