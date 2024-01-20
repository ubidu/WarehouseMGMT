using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WarehouseMGMT.Extensions;
using WarehouseMGMT.Models;
using WarehouseMGMT.Resources;
using WarehouseMGMT.Services;

namespace WarehouseMGMT.Controllers;

public class CityController : ApiController
{
    private readonly ICityService _cityService;
    private readonly ICountryService _countryService;
    private readonly IMapper _mapper;
    
    public CityController(ICityService cityService, ICountryService countryService, IMapper mapper)
    {
        _cityService = cityService;
        _countryService = countryService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<CityResource>> GetAllCitiesAsync()
    {
        var cities = await _cityService.GetAllCitiesAsync();
        var resources = new List<CityResource>();

        foreach (var city in cities)
        {
            var country = await _countryService.GetCountryByIdAsync(city.CountryId);
            var cityResource = _mapper.Map<City, CityResource>(city);
            cityResource.Country = _mapper.Map<Country, CountryResource>(country);
            resources.Add(cityResource);
        }

        return resources;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddCityAsync([FromBody] SaveCityResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetErrorMessages());
        }
        
        var city = _mapper.Map<SaveCityResource, City>(resource);
        var result = await _cityService.AddCityAsync(city);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        
        var cityResource = _mapper.Map<City, CityResource>(result.City);
        
        return Ok(cityResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCityAsync(Guid id, [FromBody] SaveCityResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetErrorMessages());
        }
        
        var city = _mapper.Map<SaveCityResource, City>(resource);
        var result = await _cityService.UpdateCityAsync(id, city);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        
        var cityResource = _mapper.Map<City, CityResource>(result.City);
        var country = await _countryService.GetCountryByIdAsync(city.CountryId);
        cityResource.Country = _mapper.Map<Country, CountryResource>(country);
        
        return Ok(cityResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCityAsync(Guid id)
    {
        var result = await _cityService.DeleteCityAsync(id);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        
        var cityResource = _mapper.Map<City, CityResource>(result.City);
        
        return Ok(cityResource);
    }
    
}