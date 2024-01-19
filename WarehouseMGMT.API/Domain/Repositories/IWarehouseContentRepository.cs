using WarehouseMGMT.Models;

namespace WarehouseMGMT.Domain.Repositories;

public interface IWarehouseContentRepository
{
    Task<IEnumerable<WarehouseContent>> GetAllWarehouseContentsAsync();
    Task<WarehouseContent> GetWarehouseContentByIdAsync(Guid id);
    Task AddWarehouseContentAsync(WarehouseContent warehouseContent);
    void Update(WarehouseContent warehouseContent);
    void Remove(WarehouseContent warehouseContent);
}