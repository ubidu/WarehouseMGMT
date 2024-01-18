namespace WarehouseMGMT.Resources;

public class SaveWarehouseResource
{
    public Guid CountryId { get; set; }
    public Guid CityId { get; set; }
    public string Address { get; set; }
    public double Capacity { get; set; }
}