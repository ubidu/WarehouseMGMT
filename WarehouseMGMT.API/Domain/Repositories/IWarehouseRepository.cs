using WarehouseMGMT.Models;

namespace WarehouseMGMT.Domain.Repositories;

public interface IWarehouseRepository
{
    Task<IEnumerable<Warehouse>> GetAllWarehousesAsync();
    Task<Warehouse> GetWarehouseByIdAsync(Guid id);
    Task AddWarehouseAsync(Warehouse warehouse);
    void Update(Warehouse warehouse);
    void Remove(Warehouse warehouse);
}