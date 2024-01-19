using WarehouseMGMT.Domain.Services.Communication;
using WarehouseMGMT.Models;

namespace WarehouseMGMT.Services;

public interface IItemService
{
    Task<IEnumerable<Item>> ListAsync();
    Task<ItemResponse> SaveAsync(Item item);
    Task<ItemResponse> UpdateAsync(Guid id, Item item);
    Task<ItemResponse> DeleteAsync(Guid id);
}