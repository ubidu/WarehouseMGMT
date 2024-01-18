using WarehouseMGMT.Models;

namespace WarehouseMGMT.Domain.Services.Communication;

public class CityResponse : BaseResponse
{
    public City City { get; set; }
    
    public CityResponse(bool success, string message, City city) : base(success, message)
    {
        City = city;
    }
    
    /// <summary>
    ///  Creates a success response.
    /// </summary>
    /// <param name="city">Saved city.</param>
    /// <returns>Response.</returns>
    public CityResponse(City city) : this(true, string.Empty, city)
    {}
    
    /// <summary>
    /// Creates an error response.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns>Response.</returns>
    public CityResponse(string message) : this(false, message, null)
    {}
}