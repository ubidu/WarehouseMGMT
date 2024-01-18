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
    private readonly IMapper _mapper;
    
    public CityController(ICityService cityService, IMapper mapper)
    {
        _cityService = cityService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<CityResource>> GetAllCitiesAsync()
    {
        var cities = await _cityService.GetAllCitiesAsync();
        var resources = _mapper.Map<IEnumerable<City>, IEnumerable<CityResource>>(cities);
        
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