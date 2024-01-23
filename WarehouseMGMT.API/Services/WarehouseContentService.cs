using WarehouseMGMT.Domain.Repositories;
using WarehouseMGMT.Domain.Services.Communication;
using WarehouseMGMT.Models;

namespace WarehouseMGMT.Services;

public class WarehouseContentService : IWarehouseContentService
{
    private readonly IWarehouseContentRepository _warehouseContentRepository;
    private readonly IWarehouseService _warehouseService;
    private readonly IItemRepository _itemRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public WarehouseContentService(IWarehouseContentRepository warehouseContentRepository, IWarehouseService warehouseService,
        IItemRepository itemRepository, IUnitOfWork unitOfWork)
    {
        _warehouseContentRepository = warehouseContentRepository;
        _warehouseService = warehouseService;
        _itemRepository = itemRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<WarehouseContent>> GetAllWarehouseContentsAsync()
    {
        return await _warehouseContentRepository.GetAllWarehouseContentsAsync();
    }
    
    public async Task<WarehouseContentResponse> GetWarehouseContentByIdAsync(Guid id)
    {
        var existingWarehouseContent = await _warehouseContentRepository.GetWarehouseContentByIdAsync(id);

        if (existingWarehouseContent == null)
        {
            return new WarehouseContentResponse("Warehouse content not found.");
        }

        return new WarehouseContentResponse(existingWarehouseContent);
    }
    
    public async Task<WarehouseContentResponse> AddWarehouseContentAsync(WarehouseContent warehouseContent)
    {
        if (warehouseContent.Quantity <= 0)
        {
            return new WarehouseContentResponse("Quantity must be greater than 0.");
        }
        
        // cant use CalculateTotalItemWeightAsync because warehouseContent.Id is still null
        var item = await _itemRepository.FindByIdAsync(warehouseContent.ItemId);
        warehouseContent.Item = item;
        var totalItemWeight = warehouseContent.Item.Weight * warehouseContent.Quantity;
        var freeSpace = await _warehouseService.CalculateFreeSpaceAsync(warehouseContent.WarehouseId);
        
        if(totalItemWeight > freeSpace)
        {
            return new WarehouseContentResponse("Not enough space in the warehouse.");
        }
        
        try
        {
            await _warehouseContentRepository.AddWarehouseContentAsync(warehouseContent);
            await _unitOfWork.CompleteAsync();
            
            return new WarehouseContentResponse(warehouseContent);
        }
        catch (Exception e)
        {
            return new WarehouseContentResponse($"An error occurred when saving the warehouse content: {e.Message}");
        }
    }
    
    public async Task<WarehouseContentResponse> UpdateWarehouseContentAsync(Guid id, WarehouseContent warehouseContent)
    {
        var existingWarehouseContent = await _warehouseContentRepository.GetWarehouseContentByIdAsync(id);

        if (existingWarehouseContent == null)
        {
            return null;
        }
        
        existingWarehouseContent.ItemId = warehouseContent.ItemId;
        existingWarehouseContent.WarehouseId = warehouseContent.WarehouseId;
        existingWarehouseContent.Quantity = warehouseContent.Quantity;
        
        try
        {
            _warehouseContentRepository.Update(existingWarehouseContent);
            await _unitOfWork.CompleteAsync();
            
            return new WarehouseContentResponse(existingWarehouseContent);
        }
        catch (Exception e)
        {
            return new WarehouseContentResponse($"An error occurred when updating the warehouse content: {e.Message}");
        }
    }
    
    public async Task<WarehouseContentResponse> DeleteWarehouseContentAsync(Guid id)
    {
        var existingWarehouseContent = await _warehouseContentRepository.GetWarehouseContentByIdAsync(id);

        if (existingWarehouseContent == null)
        {
            return null;
        }
        
        try
        {
            _warehouseContentRepository.Remove(existingWarehouseContent);
            await _unitOfWork.CompleteAsync();
            
            return new WarehouseContentResponse(existingWarehouseContent);
        }
        catch (Exception e)
        {
            return new WarehouseContentResponse($"An error occurred when deleting the warehouse content: {e.Message}");
        }
    }

    public async Task<double> CalculateTotalItemWeightAsync(Guid id)
    {
        var warehouseContent = await _warehouseContentRepository.GetWarehouseContentByIdAsync(id);

        if (warehouseContent == null)
        {
            throw new Exception("Warehouse content not found.");
        }
        
        var item = warehouseContent.Item;
        
        return item.Weight * warehouseContent.Quantity;
    }
}