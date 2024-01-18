using WarehouseMGMT.Models;

namespace WarehouseMGMT.Domain.Services.Communication;

public class CountryResponse : BaseResponse
{
    public Country Country { get; set; }
    
    private CountryResponse(bool success, string message, Country country) : base(success, message)
    {
        Country = country;
    }
    
    
    /// <summary>
    ///  Creates a success response.
    /// </summary>
    /// <param name="country">Saved country.</param>
    /// <returns>Response.</returns>
    public CountryResponse(Country country) : this(true, string.Empty, country)
    {}
    
    /// <summary>
    /// Creates an error response.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns>Response.</returns>
    public CountryResponse(string message) : this(false, message, null)
    {}
}