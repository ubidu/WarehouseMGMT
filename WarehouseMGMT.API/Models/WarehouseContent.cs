namespace WarehouseMGMT.Models;

public class WarehouseContent
{
    public Guid Id { get; private set; }
    public Guid ItemId { get; set; }
    public int Quantity { get; set; }
    
    public Guid WarehouseId { get; set; }

    public virtual Warehouse Warehouse { get; private set; }
    public virtual Item Item { get; private set; }
}