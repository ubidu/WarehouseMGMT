using System.ComponentModel.DataAnnotations;

namespace WarehouseMGMT.Resources;

public class SaveCountryResource
{
    [Required]
    [MaxLength(35)]
    public string Name { get; set; }
}