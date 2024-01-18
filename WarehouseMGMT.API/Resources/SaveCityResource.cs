using System.ComponentModel.DataAnnotations;

namespace WarehouseMGMT.Resources;

public class SaveCityResource
{
    [Required] [MaxLength(35)] public string Name { get; set; }
    [Required] public Guid CountryId { get; set; } 
}