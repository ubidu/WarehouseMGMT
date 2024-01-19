using System.ComponentModel.DataAnnotations;

namespace WarehouseMGMT.Resources;

public class SaveWarehouseResource
{
    [Required] public Guid CountryId { get; set; }
    [Required] public Guid CityId { get; set; }
    [Required] [MaxLength(50)] public string Address { get; set; }
    [Required] public double Capacity { get; set; }
}