using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WarehouseMGMT.Extensions;
using WarehouseMGMT.Models;
using WarehouseMGMT.Resources;
using WarehouseMGMT.Services;

namespace WarehouseMGMT.Controllers;

public class ItemController : ApiController
{
    private readonly IItemService _itemService;
    private readonly IMapper _mapper;

    public ItemController(IItemService itemService, IMapper mapper)
    {
        _itemService = itemService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ItemResource>> GetAllAsync()
    {
        var items = await _itemService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Item>, IEnumerable<ItemResource>>(items);

        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveItemResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var item = _mapper.Map<SaveItemResource, Item>(resource);
        var result = await _itemService.SaveAsync(item);

        if (!result.Success)
            return BadRequest(result.Message);

        var itemResource = _mapper.Map<Item, ItemResource>(result.Item);
        return Ok(itemResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(Guid id, [FromBody] SaveItemResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var item = _mapper.Map<SaveItemResource, Item>(resource);
        var result = await _itemService.UpdateAsync(id, item);

        if (!result.Success)
            return BadRequest(result.Message);

        var itemResource = _mapper.Map<Item, ItemResource>(result.Item);
        return Ok(itemResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _itemService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var itemResource = _mapper.Map<Item, ItemResource>(result.Item);
        return Ok(itemResource);
    }
    
}