using WarehouseMGMT.Domain.Services.Communication;

namespace WarehouseMGMT.Resources;

public class WarehouseContentResource
{
    public Guid Id { get; set; }
    public ItemResponse Item { get; set; }
    public int Quantity { get; set; }
    public WarehouseResponse Warehouse { get; set; }
    public double TotalWeight { get; set; }
}