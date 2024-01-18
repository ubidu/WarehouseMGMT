namespace WarehouseMGMT.Models;

public class City
{
    public Guid Id { get; private set; }
    public string Name { get; set; }
    public Guid CountryId { get; set; }
    
    public virtual ICollection<Warehouse> Warehouses { get; private set; } = null!;
    public virtual Country Country { get; set; } = null!;
}