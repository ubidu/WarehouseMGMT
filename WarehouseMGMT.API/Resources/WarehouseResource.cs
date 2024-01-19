namespace WarehouseMGMT.Resources;

public class WarehouseResource
{
    public CountryResource Country { get; set; }
    public CityResource City { get; set; }
    public string Address { get; set; }
    public double Capacity { get; set; }
    public double UsedCapacity { get; set; }
    public double FreeCapacity { get; set; }
    public bool IsFull { get; set; }
}