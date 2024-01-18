using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WarehouseMGMT.Models;
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
    public async Task<IEnumerable<City>> GetAllCitiesAsync()
    {
        var cities = await _cityService.GetAllCitiesAsync();
        var resources = _mapper.Map<IEnumerable<City>, IEnumerable<City>>(cities);
        
        return cities;
    }
    
}