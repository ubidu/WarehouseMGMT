namespace WarehouseMGMT.Models;

public class Warehouse
{
    public Guid Id { get; private set; }
    public Guid CountryId { get; set; }
    public Guid CityId { get; set; }
    public string Address { get; set; }
    public double Capacity { get; set; }
    
    public virtual ICollection<WarehouseContent> WarehouseContents { get; private set; } = null!;
    public virtual Country Country { get; private set; } = null!;
    public virtual City City { get; private set; } = null!;

    public Warehouse()
    { }
    
    public Warehouse(Guid id)
    {
        Id = id;
    }
}