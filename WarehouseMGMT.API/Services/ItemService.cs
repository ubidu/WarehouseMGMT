using WarehouseMGMT.Domain.Repositories;
using WarehouseMGMT.Domain.Services.Communication;
using WarehouseMGMT.Models;

namespace WarehouseMGMT.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public ItemService(IItemRepository itemRepository, IUnitOfWork unitOfWork)
    {
        _itemRepository = itemRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<Item>> ListAsync()
    {
        return await _itemRepository.ListAsync();
    }
    
    public async Task<Item> GetItemByIdAsync(Guid id)
    {
        return await _itemRepository.FindByIdAsync(id);
    }

    public async Task<ItemResponse> SaveAsync(Item item)
    {
        var weight = item.Weight;
        
        if (weight <= 0)
        {
            return new ItemResponse("Item weight must be greater than 0.");
        }
        
        try
        {
            await _itemRepository.Add(item);
            await _unitOfWork.CompleteAsync();
            
            return new ItemResponse(item);
        }
        catch (Exception e)
        {
            return new ItemResponse($"An error occurred when saving the item: {e.Message}");
        }
    }

    public async Task<ItemResponse> UpdateAsync(Guid id, Item item)
    {
        var existingItem = await _itemRepository.FindByIdAsync(id);

        if (existingItem == null)
        {
            return new ItemResponse("Item not found.");
        }
        
        var weight = item.Weight;
        
        if (weight <= 0)
        {
            return new ItemResponse("Item weight must be greater than 0.");
        }
        
        existingItem.Name = item.Name;
        existingItem.Weight = item.Weight;
        
        try
        {
            _itemRepository.Update(existingItem);
            await _unitOfWork.CompleteAsync();
            
            return new ItemResponse(existingItem);
        }
        catch (Exception e)
        {
            return new ItemResponse($"An error occurred when updating the item: {e.Message}");
        }
    }

    public async Task<ItemResponse> DeleteAsync(Guid id)
    {
        var existingItem = await _itemRepository.FindByIdAsync(id);
        
        if (existingItem == null)
        {
            return new ItemResponse("Item not found.");
        }
        
        try
        {
            _itemRepository.Remove(existingItem);
            await _unitOfWork.CompleteAsync();
            
            return new ItemResponse(existingItem);
        }
        catch (Exception e)
        {
            return new ItemResponse($"An error occurred when deleting the item: {e.Message}");
        }
    }
}