using WarehouseMGMT.Models;

namespace WarehouseMGMT.Domain.Services.Communication;

public class WarehouseContentResponse : BaseResponse
{
    public WarehouseContent WarehouseContent { get; set; }
    
    private WarehouseContentResponse(bool success, string message, WarehouseContent warehouseContent) : base(success, message)
    {
        WarehouseContent = warehouseContent;
    }
    
    /// <summary>
    ///  Creates a success response.
    /// </summary>
    /// <param name="warehouseContent">Saved warehouse content.</param>
    /// <returns>Response.</returns>
    public WarehouseContentResponse(WarehouseContent warehouseContent) : this(true, string.Empty, warehouseContent)
    {}
    
    /// <summary>
    /// Creates an error response.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns>Response.</returns>
    public WarehouseContentResponse(string message) : this(false, message, null)
    {}
}