using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WarehouseMGMT.Extensions;
using WarehouseMGMT.Models;
using WarehouseMGMT.Resources;
using WarehouseMGMT.Services;

namespace WarehouseMGMT.Controllers;

public class WarehouseContentController : ApiController
{
    private readonly IWarehouseContentService _warehouseContentService;
    private readonly IMapper _mapper;
    
    public WarehouseContentController(IWarehouseContentService warehouseContentService, IMapper mapper)
    {
        _warehouseContentService = warehouseContentService;
        _mapper = mapper;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddWarehouseContentAsync([FromBody] SaveWarehouseContentResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetErrorMessages());
        }
        
        var warehouseContent = _mapper.Map<SaveWarehouseContentResource, WarehouseContent>(resource);
        var result = await _warehouseContentService.AddWarehouseContentAsync(warehouseContent);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        
        var warehouseContentResource = _mapper.Map<WarehouseContent, WarehouseContentResource>(result.WarehouseContent);

        return Ok(warehouseContentResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWarehouseContentAsync(Guid id, [FromBody] SaveWarehouseContentResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetErrorMessages());
        }
        
        var warehouseContent = _mapper.Map<SaveWarehouseContentResource, WarehouseContent>(resource);
        var result = await _warehouseContentService.UpdateWarehouseContentAsync(id, warehouseContent);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        
        var warehouseContentResource = _mapper.Map<WarehouseContent, WarehouseContentResource>(result.WarehouseContent);

        return Ok(warehouseContentResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWarehouseContentAsync(Guid id)
    {
        var result = await _warehouseContentService.DeleteWarehouseContentAsync(id);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        var warehouseContentResource = _mapper.Map<WarehouseContent, WarehouseContentResource>(result.WarehouseContent);

        return Ok(warehouseContentResource);
    }
}