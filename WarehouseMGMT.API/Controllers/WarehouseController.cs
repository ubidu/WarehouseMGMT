using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WarehouseMGMT.Extensions;
using WarehouseMGMT.Models;
using WarehouseMGMT.Resources;
using WarehouseMGMT.Services;

namespace WarehouseMGMT.Controllers;

public class WarehouseController : ApiController
{
    private readonly IWarehouseService _warehouseService;
    private readonly ICountryService _countryService;
    private readonly ICityService _cityService;
    private readonly IItemService _itemService;
    private readonly IWarehouseContentService _warehouseContentService;
    private readonly IMapper _mapper;
    
    public WarehouseController(IWarehouseService warehouseService, ICountryService countryService,
        ICityService cityService, IItemService itemService, IWarehouseContentService warehouseContentService,  IMapper mapper)
    {
        _warehouseService = warehouseService;
        _countryService = countryService;
        _cityService = cityService;
        _itemService = itemService;
        _warehouseContentService = warehouseContentService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<WarehouseResource>> GetAllWarehousesAsync()
    {
        var warehouses = await _warehouseService.GetAllWarehousesAsync();
        var resources = new List<WarehouseResource>();
        
        foreach (var warehouse in warehouses)
        {
            var country = await _countryService.GetCountryByIdAsync(warehouse.CountryId);
            var city = await _cityService.GetCityByIdAsync(warehouse.CityId);
            var warehouseResource = _mapper.Map<Warehouse, WarehouseResource>(warehouse);
            warehouseResource.Country = _mapper.Map<Country, CountryResource>(country);
            warehouseResource.City = _mapper.Map<City, CityResource>(city);
            warehouseResource.UsedCapacity = await _warehouseService.CalculateUsedSpaceAsync(warehouse.Id);
            warehouseResource.FreeCapacity = await _warehouseService.CalculateFreeSpaceAsync(warehouse.Id);
            warehouseResource.IsFull = await _warehouseService.CheckIfWarehouseIsFullAsync(warehouse.Id);
            resources.Add(warehouseResource);
        }
        
        
        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetWarehouseByIdAsync(Guid id)
    {
        var result = await _warehouseService.GetWarehouseByIdAsync(id);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        var warehouseResource = _mapper.Map<Warehouse, WarehouseResource>(result.Warehouse);

        var country = await _countryService.GetCountryByIdAsync(result.Warehouse.CountryId);
        var city = await _cityService.GetCityByIdAsync(result.Warehouse.CityId);
        warehouseResource.Country = _mapper.Map<Country, CountryResource>(country);
        warehouseResource.City = _mapper.Map<City, CityResource>(city);
        warehouseResource.UsedCapacity = await _warehouseService.CalculateUsedSpaceAsync(id);
        warehouseResource.FreeCapacity = await _warehouseService.CalculateFreeSpaceAsync(id);
        warehouseResource.IsFull = await _warehouseService.CheckIfWarehouseIsFullAsync(id);

        return Ok(warehouseResource);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddWarehouseAsync([FromBody] SaveWarehouseResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetErrorMessages());
        }   
        
        var warehouse = _mapper.Map<SaveWarehouseResource, Warehouse>(resource);
        var result = await _warehouseService.AddWarehouseAsync(warehouse);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        
        var warehouseResource = _mapper.Map<Warehouse, WarehouseResource>(result.Warehouse);
        
        return Ok(warehouseResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWarehouseAsync(Guid id, [FromBody] SaveWarehouseResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetErrorMessages());
        }
        
        var warehouse = _mapper.Map<SaveWarehouseResource, Warehouse>(resource);
        var result = await _warehouseService.UpdateWarehouseAsync(id, warehouse);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        
        var warehouseResource = _mapper.Map<Warehouse, WarehouseResource>(result.Warehouse);
        
        return Ok(warehouseResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWarehouseAsync(Guid id)
    {
        var result = await _warehouseService.DeleteWarehouseAsync(id);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        
        var warehouseResource = _mapper.Map<Warehouse, WarehouseResource>(result.Warehouse);
        
        return Ok(warehouseResource);
    }
    
    [HttpGet("{id}/content")]
    public async Task<IEnumerable<WarehouseContentResource>> GetWarehouseContentAsync(Guid id)
    {
        var warehouseContents = await _warehouseService.GetWarehouseContentAsync(id);
        var resources = new List<WarehouseContentResource>();
        
        foreach (var warehouseContent in warehouseContents)
        {
            var warehouseContentResource = _mapper.Map<WarehouseContent, WarehouseContentResource>(warehouseContent);
            var item = await _itemService.GetItemByIdAsync(warehouseContent.ItemId);
            warehouseContentResource.Item = _mapper.Map<Item, ItemResource>(item);
            warehouseContentResource.TotalWeight =
                await _warehouseContentService.CalculateTotalItemWeightAsync(warehouseContent.Id);
            resources.Add(warehouseContentResource);
        }
        
        return resources;
    }
}