using WarehouseMGMT.Domain.Repositories;
using WarehouseMGMT.Domain.Services.Communication;
using WarehouseMGMT.Models;

namespace WarehouseMGMT.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IWarehouseContentRepository _warehouseContentRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public WarehouseService(IWarehouseRepository warehouseRepository, IWarehouseContentRepository warehouseContentRepository, IItemRepository itemRepository,
        ICountryRepository countryRepository, ICityRepository cityRepository ,IUnitOfWork unitOfWork)
    {
        _warehouseRepository = warehouseRepository;
        _warehouseContentRepository = warehouseContentRepository;
        _countryRepository = countryRepository;
        _cityRepository = cityRepository;
        _itemRepository = itemRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<Warehouse>> GetAllWarehousesAsync()
    {
        return await _warehouseRepository.GetAllWarehousesAsync();
    }

    public async Task<WarehouseResponse> GetWarehouseByIdAsync(Guid id)
    {
        var existingWarehouse = await _warehouseRepository.GetWarehouseByIdAsync(id);

        if (existingWarehouse == null)
        {
            return new WarehouseResponse("Warehouse not found.");
        }

        return new WarehouseResponse(existingWarehouse);
    }

    public async Task<WarehouseResponse> AddWarehouseAsync(Warehouse warehouse)
    {
        var city = await _cityRepository.GetCityByIdAsync(warehouse.CityId);
        
        if (city == null)
        {
            return new WarehouseResponse("City not found.");
        }
        
        var country = await _countryRepository.GetCountryByIdAsync(city.CountryId);
        
        warehouse.CountryId = country.Id;
        
        if(warehouse.Capacity <= 0)
        {
            return new WarehouseResponse("Warehouse capacity must be greater than 0.");
        }
        
        try
        {
            await _warehouseRepository.AddWarehouseAsync(warehouse);
            await _unitOfWork.CompleteAsync();
            
            return new WarehouseResponse(warehouse);
        }
        catch (Exception e)
        {
            return new WarehouseResponse($"An error occurred when saving the warehouse: {e.Message}");
        }
    }

    public async Task<WarehouseResponse> UpdateWarehouseAsync(Guid id, Warehouse warehouse)
    {
        var existingWarehouse = await _warehouseRepository.GetWarehouseByIdAsync(id);
        
        if (existingWarehouse == null)
        {
            return new WarehouseResponse("Warehouse not found.");
        }
        
        var city = await _cityRepository.GetCityByIdAsync(warehouse.CityId);
        
        if (city == null)
        {
            return new WarehouseResponse("City not found.");
        }
        
        var country = await _countryRepository.GetCountryByIdAsync(city.CountryId);
        
        if(warehouse.Capacity <= 0)
        {
            return new WarehouseResponse("Warehouse capacity must be greater than 0.");
        }
        
        existingWarehouse.CountryId = country.Id;
        existingWarehouse.CityId = warehouse.CityId;
        existingWarehouse.Address = warehouse.Address;
        existingWarehouse.Capacity = warehouse.Capacity;
        
        try
        {
            _warehouseRepository.Update(existingWarehouse);
            await _unitOfWork.CompleteAsync();
            
            return new WarehouseResponse(existingWarehouse);
        }
        catch (Exception e)
        {
            return new WarehouseResponse($"An error occurred when updating the warehouse: {e.Message}");
        }
    }

    public async Task<WarehouseResponse> DeleteWarehouseAsync(Guid id)
    {
        var existingWarehouse = await _warehouseRepository.GetWarehouseByIdAsync(id);
        
        if (existingWarehouse == null)
        {
            return new WarehouseResponse("Warehouse not found.");
        }
        
        try
        {
            _warehouseRepository.Remove(existingWarehouse);
            await _unitOfWork.CompleteAsync();
            
            return new WarehouseResponse(existingWarehouse);
        }
        catch (Exception e)
        {
            return new WarehouseResponse($"An error occurred when deleting the warehouse: {e.Message}");
        }
    }

    public async Task<double> CalculateUsedSpaceAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new Exception("Warehouse id is empty.");
        }
        
        if (id == null)
        {
            throw new Exception("Warehouse id is null.");
        }
        
        var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(id);
        
        if (warehouse == null)
        {
            throw new Exception("Warehouse not found.");
        }

        var warehouseContents = await _warehouseContentRepository.GetAllWarehouseContentsAsync();
        
        if (warehouseContents == null)
        {
            throw new Exception("Warehouse content not found.");
        }
        
        var totalWeight = 0.0;
        
        foreach (var warehouseContent in warehouseContents)
        {
            warehouseContent.Item = await _itemRepository.FindByIdAsync(warehouseContent.ItemId);
            totalWeight += warehouseContent.Quantity * warehouseContent.Item.Weight;
        }
        
        return totalWeight;
    }

    public async Task<double> CalculateFreeSpaceAsync(Guid id)
    {
        var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(id);
        
        if (warehouse == null)
        {
            throw new Exception("Warehouse not found.");
        }
        
        var usedSpace = await CalculateUsedSpaceAsync(id);
        
        return warehouse.Capacity - usedSpace;
    }

    public async Task<bool> CheckIfWarehouseIsFullAsync(Guid id)
    {
        var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(id);
        
        if (warehouse == null)
        {
            throw new Exception("Warehouse not found.");
        }
        
        var freeSpace = await CalculateFreeSpaceAsync(id);
        
        return freeSpace == 0;
    }
    
    public async Task<IEnumerable<WarehouseContent>> GetWarehouseContentAsync(Guid id)
    {
        var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(id);
        
        if (warehouse == null)
        {
            throw new Exception("Warehouse not found.");
        }
        
        var warehouseContents = await _warehouseContentRepository.GetAllWarehouseContentsAsync();
        
        if (warehouseContents == null)
        {
            throw new Exception("Warehouse content not found.");
        }
        
        return warehouseContents;
    }
}