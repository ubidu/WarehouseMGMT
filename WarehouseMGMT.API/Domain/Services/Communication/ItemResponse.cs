using WarehouseMGMT.Models;

namespace WarehouseMGMT.Domain.Services.Communication;

public class ItemResponse : BaseResponse
{
    public Item Item { get; set; }
    
    private ItemResponse(bool success, string message, Item item) : base(success, message)
    {
        Item = item;
    }

    /// <summary>
    ///  Creates a success response.
    /// </summary>
    /// <param name="item">Saved item.</param>
    /// <returns>Response.</returns>
    public ItemResponse(Item item) : this(true, string.Empty, item)
    {}
    
    /// <summary>
    /// Creates an error response.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns>Response.</returns>
    public ItemResponse(string message) : this(false, message, null)
    {}

    
}