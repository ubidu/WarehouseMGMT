using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseMGMT.Extensions;
using WarehouseMGMT.Models;
using WarehouseMGMT.Persistence;
using WarehouseMGMT.Resources;
using WarehouseMGMT.Services;

namespace WarehouseMGMT.Controllers;

public class CountryController : ApiController
{
    private readonly ICountryService _countryCountryService;
    private readonly IMapper _mapper;
    
    public CountryController(ICountryService countryService, IMapper mapper)
    {
        _countryCountryService = countryService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<CountryResource>> GetAllCountriesAsync()
    {
        var countries = await _countryCountryService.GetAllCountriesAsync();
        var resources = _mapper.Map<IEnumerable<Country>, IEnumerable<CountryResource>>(countries);
        
        return resources;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddCountryAsync([FromBody] SaveCountryResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetErrorMessages());
        }
        
        var country = _mapper.Map<SaveCountryResource, Country>(resource);
        var result = await _countryCountryService.AddCountryAsync(country);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        
        var countryResource = _mapper.Map<Country, CountryResource>(result.Country);
        
        return Ok(countryResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCountryAsync(Guid id, [FromBody] SaveCountryResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetErrorMessages());
        }
        
        var country = _mapper.Map<SaveCountryResource, Country>(resource);
        var result = await _countryCountryService.UpdateCountryAsync(id, country);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        
        var countryResource = _mapper.Map<Country, CountryResource>(result.Country);
        
        return Ok(countryResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountryAsync(Guid id)
    {
        var result = await _countryCountryService.DeleteCountryAsync(id);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        
        var countryResource = _mapper.Map<Country, CountryResource>(result.Country);
        
        return Ok(countryResource);
    }
}