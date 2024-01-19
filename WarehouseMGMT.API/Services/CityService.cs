using WarehouseMGMT.Domain.Repositories;
using WarehouseMGMT.Domain.Services.Communication;
using WarehouseMGMT.Models;

namespace WarehouseMGMT.Services;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CityService(ICityRepository cityRepository, ICountryRepository countryRepository, IUnitOfWork unitOfWork)
    {
        _cityRepository = cityRepository;
        _countryRepository = countryRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<City>> GetAllCitiesAsync()
    {
        return await _cityRepository.GetAllCitiesAsync();   
    }

    public async Task<CityResponse> AddCityAsync(City city)
    {
        var existingCountry = await _countryRepository.GetCountryByIdAsync(city.CountryId);
            
        if (existingCountry == null)
        {
            return new CityResponse("Country not found.");
        }
    
        
        try
        {
            await _cityRepository.AddCityAsync(city);
            await _unitOfWork.CompleteAsync();
            
            return new CityResponse(city);
        }
        catch (Exception e)
        {
            return new CityResponse($"An error occurred when saving the city: {e.Message}");
        }
    }

    public async Task<CityResponse> UpdateCityAsync(Guid id, City city)
    {
        var existingCity = await _cityRepository.GetCityByIdAsync(id);
        
        if (existingCity == null)
        {
            return new CityResponse("City not found.");
        }
        
        existingCity.Name = city.Name;
        
        try
        {
            _cityRepository.Update(existingCity);
            await _unitOfWork.CompleteAsync();
            
            return new CityResponse(existingCity);
        }
        catch (Exception e)
        {
            return new CityResponse($"An error occurred when updating the city: {e.Message}");
        }
    }

    public async Task<CityResponse> DeleteCityAsync(Guid id)
    {
        var existingCity = await _cityRepository.GetCityByIdAsync(id);
        
        if (existingCity == null)
        {
            return new CityResponse("City not found.");
        }
        
        try
        {
            _cityRepository.Remove(existingCity);
            await _unitOfWork.CompleteAsync();
            
            return new CityResponse(existingCity);
        }
        catch (Exception e)
        {
            return new CityResponse($"An error occurred when deleting the city: {e.Message}");
        }
    }
}