using Microsoft.EntityFrameworkCore;
using WarehouseMGMT.Domain.Repositories;
using WarehouseMGMT.Models;

namespace WarehouseMGMT.Persistence.Repository;

public class ItemRepository : BaseRepository, IItemRepository
{
    public ItemRepository(WarehouseMGMTDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Item>> ListAsync()
    {
        return await _context.Items.ToListAsync();
    }

    public async Task<Item?> FindByIdAsync(Guid id)
    {
        return await _context.Items.FindAsync(id);
    }

    public async Task Add(Item item)
    {
        await _context.Items.AddAsync(item);
    }

    public void Update(Item item)
    {
        _context.Items.Update(item);
    }

    public void Remove(Item item)
    {
        _context.Items.Remove(item);
    }
    
}