using System.ComponentModel.DataAnnotations;

namespace WarehouseMGMT.Resources;

public class SaveItemResource
{
    [Required] [MaxLength(50)] public string Name { get; set; }
    [Required] public double Weight { get; set; }
}