using WarehouseMGMT.Models;

namespace WarehouseMGMT.Domain.Repositories;

public interface IItemRepository
{
    Task<IEnumerable<Item>> ListAsync();
    Task<Item?> FindByIdAsync(Guid id);
    Task Add(Item item);
    void Update(Item item);
    void Remove(Item item);
}