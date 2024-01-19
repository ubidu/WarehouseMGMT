using WarehouseMGMT.Domain.Repositories;
using WarehouseMGMT.Domain.Services.Communication;
using WarehouseMGMT.Models;

namespace WarehouseMGMT.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CountryService(ICountryRepository countryRepository, IUnitOfWork unitOfWork)
    {
        _countryRepository = countryRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<Country>> GetAllCountriesAsync()
    {
        return await _countryRepository.GetAllCountriesAsync();
    }
    
    public async Task<Country> GetCountryByIdAsync(Guid id)
    {
        var existingCountry = await _countryRepository.GetCountryByIdAsync(id);

        if (existingCountry == null)
        {
            throw new Exception("Country not found.");
        }
        
        return existingCountry;
    }
    
    public async Task<CountryResponse> AddCountryAsync(Country country)
    {
        try
        {
            await _countryRepository.AddCountryAsync(country);
            await _unitOfWork.CompleteAsync();
            
            return new CountryResponse(country);
        }
        catch (Exception e)
        {
            return new CountryResponse($"An error occurred when saving the country: {e.Message}");
        }
    }

    public async Task<CountryResponse> UpdateCountryAsync(Guid id, Country country)
    {
        var existingCountry = await _countryRepository.GetCountryByIdAsync(id);

        if (existingCountry == null)
        {
            return new CountryResponse("Country not found.");
        }
        
        existingCountry.Name = country.Name;
        
        try
        {
            _countryRepository.Update(existingCountry);
            await _unitOfWork.CompleteAsync();
            
            return new CountryResponse(existingCountry);
        }
        catch (Exception e)
        {
            return new CountryResponse($"An error occurred when updating the country: {e.Message}");
        }
    }

    public async Task<CountryResponse> DeleteCountryAsync(Guid id)
    {
        var existingCountry = await _countryRepository.GetCountryByIdAsync(id);
        
        if (existingCountry == null)
        {
            return new CountryResponse("Country not found.");
        }
        
        try
        {
            _countryRepository.Remove(existingCountry);
            await _unitOfWork.CompleteAsync();
            
            return new CountryResponse(existingCountry);
        }
        catch (Exception e)
        {
            return new CountryResponse($"An error occurred when deleting the country: {e.Message}");
        }
    }   
}