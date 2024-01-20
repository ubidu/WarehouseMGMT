using System.ComponentModel.DataAnnotations;

namespace WarehouseMGMT.Resources;

public class SaveWarehouseContentResource
{
    [Required]
    public Guid WarehouseId { get; set; }
    
    [Required]
    public Guid ItemId { get; set; }
    
    [Required]
    public int Quantity { get; set; }
}