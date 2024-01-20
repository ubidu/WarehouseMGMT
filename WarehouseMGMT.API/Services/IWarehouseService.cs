using WarehouseMGMT.Domain.Services.Communication;
using WarehouseMGMT.Models;

namespace WarehouseMGMT.Services;

public interface IWarehouseService
{
    Task<IEnumerable<Warehouse>> GetAllWarehousesAsync();
    Task<WarehouseResponse> GetWarehouseByIdAsync(Guid id);
    Task<WarehouseResponse> AddWarehouseAsync(Warehouse warehouse);
    Task<WarehouseResponse> UpdateWarehouseAsync(Guid id, Warehouse warehouse);
    Task<WarehouseResponse> DeleteWarehouseAsync(Guid id);
    Task<double> CalculateUsedSpaceAsync(Guid id);
    Task<double> CalculateFreeSpaceAsync(Guid id);
    Task<bool> CheckIfWarehouseIsFullAsync(Guid id);
    Task<IEnumerable<WarehouseContent>> GetWarehouseContentAsync(Guid id);
}
