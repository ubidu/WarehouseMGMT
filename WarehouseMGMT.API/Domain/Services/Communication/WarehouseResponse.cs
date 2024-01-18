using WarehouseMGMT.Models;

namespace WarehouseMGMT.Domain.Services.Communication;

public class WarehouseResponse
{
    public Warehouse Warehouse { get; set; }
    
    private WarehouseResponse(bool success, string message, Warehouse warehouse)
    {
        Warehouse = warehouse;
    }
    
    
    /// <summary>
    ///  Creates a success response.
    /// </summary>
    /// <param name="warehouse">Saved warehouse.</param>
    /// <returns>Response.</returns>
    public WarehouseResponse(Warehouse warehouse) : this(true, string.Empty, warehouse)
    {}
    
    /// <summary>
    /// Creates an error response.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns>Response.</returns>
    public WarehouseResponse(string message) : this(false, message, null)
    {}
}