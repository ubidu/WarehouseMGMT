using WarehouseMGMT.Domain.Services.Communication;
using WarehouseMGMT.Models;

namespace WarehouseMGMT.Services;

public interface IWarehouseContentService
{
    Task<IEnumerable<WarehouseContent>> GetAllWarehouseContentsAsync();
    Task<WarehouseContentResponse> GetWarehouseContentByIdAsync(Guid id);
    Task<WarehouseContentResponse> AddWarehouseContentAsync(WarehouseContent warehouseContent);
    Task<WarehouseContentResponse> UpdateWarehouseContentAsync(Guid id, WarehouseContent warehouseContent);
    Task<WarehouseContentResponse> DeleteWarehouseContentAsync(Guid id);
    Task<double> CalculateTotalWeightAsync(Guid id);
}