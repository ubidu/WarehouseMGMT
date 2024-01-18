namespace WarehouseMGMT.Models;

public class Country
{
    public Guid Id { get; private set; }
    public string Name { get; set; }
    
    public virtual ICollection<City> Cities { get; private set; } = null!;
    public virtual ICollection<Warehouse> Warehouses { get; private set; } = null!;
}