using Moq;
using WarehouseMGMT.Domain.Repositories;
using WarehouseMGMT.Models;
using WarehouseMGMT.Services;

namespace WarehouseMGMT.Tests;

public class WarehouseTest
{
    private readonly IWarehouseService _warehouseService;
    private readonly Mock<IWarehouseRepository> _warehouseRepositoryMock;
    private readonly Mock<IWarehouseContentRepository> _warehouseContentRepositoryMock;
    private readonly Mock<IItemRepository> _itemRepositoryMock;
    private readonly Mock<ICountryRepository> _countryRepositoryMock;
    private readonly Mock<ICityRepository> _cityRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    
    public WarehouseTest()
    {
        _warehouseRepositoryMock = new Mock<IWarehouseRepository>();
        _warehouseContentRepositoryMock = new Mock<IWarehouseContentRepository>();
        _itemRepositoryMock = new Mock<IItemRepository>();
        _countryRepositoryMock = new Mock<ICountryRepository>();
        _cityRepositoryMock = new Mock<ICityRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _warehouseService = new WarehouseService(_warehouseRepositoryMock.Object, 
            _warehouseContentRepositoryMock.Object, _itemRepositoryMock.Object,
            _countryRepositoryMock.Object, _cityRepositoryMock.Object, _unitOfWorkMock.Object);
    }
    
    [Fact]
    public async Task Get_Warehouse_Content_Test()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var warehouseContents = new List<WarehouseContent>
        {
            new WarehouseContent { WarehouseId = warehouseId, ItemId = Guid.NewGuid(), Quantity = 10 },
            new WarehouseContent { WarehouseId = warehouseId, ItemId = Guid.NewGuid(), Quantity = 20 }
        };

        _warehouseRepositoryMock.Setup(x => x.GetWarehouseByIdAsync(warehouseId)).ReturnsAsync(new Warehouse(warehouseId));
        _warehouseContentRepositoryMock.Setup(x => x.GetAllWarehouseContentsAsync()).ReturnsAsync(warehouseContents);

        // Act
        var result = await _warehouseService.GetWarehouseContentAsync(warehouseId);

        // Assert
        Assert.Equal(warehouseContents, result);
    }
    
    [Fact]
    public async Task Add_Warehouse_Correct_Test()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var countryId = Guid.NewGuid();
        var cityId = Guid.NewGuid();
        var warehouse = new Warehouse(warehouseId) 
        { 
            Address = "Test Address", 
            Capacity = 1000.0, 
            CountryId = countryId, 
            CityId = cityId 
        };

        // Act
        var result = await _warehouseService.AddWarehouseAsync(warehouse);

        // Assert
        Assert.Equal(warehouse, result.Warehouse);
        Assert.Equal(countryId, result.Warehouse.CountryId);
        Assert.Equal(cityId, result.Warehouse.CityId);
    }
    
    [Fact]
    public async Task Add_Warehouse_Incorrect_Test()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var countryId = Guid.NewGuid();
        var cityId = Guid.NewGuid();
        var warehouse = new Warehouse(warehouseId) 
        { 
            Address = "Test Address", 
            Capacity = 1000.0, 
            CountryId = countryId, 
            CityId = cityId 
        };

        // Act
        var result = await _warehouseService.AddWarehouseAsync(warehouse);

        // Assert
        Assert.NotEqual(Guid.NewGuid(), result.Warehouse.Id);
        Assert.NotEqual(Guid.NewGuid(), result.Warehouse.CountryId);
        Assert.NotEqual(Guid.NewGuid(), result.Warehouse.CityId);
    }
    
    [Fact]
    public async Task Update_Warehouse_Correct_Test()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var countryId = Guid.NewGuid();
        var cityId = Guid.NewGuid();
        var warehouse = new Warehouse(warehouseId)
        {
            Address = "Test Address",
            Capacity = 1000.0,
            CountryId = countryId,
            CityId = cityId
        };

        _warehouseRepositoryMock.Setup(x => x.GetWarehouseByIdAsync(warehouseId)).ReturnsAsync(warehouse);
        _warehouseRepositoryMock.Setup(x => x.Update(warehouse));
        _unitOfWorkMock.Setup(x => x.CompleteAsync());

        // Act
        var result = await _warehouseService.UpdateWarehouseAsync(warehouseId, warehouse);

        // Assert
        Assert.Equal(warehouse, result.Warehouse);
        Assert.Equal(countryId, result.Warehouse.CountryId);
        Assert.Equal(cityId, result.Warehouse.CityId);
    }
    
    [Fact]
    public async Task Update_Warehouse_Incorrect_Test()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var countryId = Guid.NewGuid();
        var cityId = Guid.NewGuid();
        var warehouse = new Warehouse(warehouseId) 
        { 
            Address = "Test Address", 
            Capacity = 1000.0, 
            CountryId = countryId, 
            CityId = cityId 
        };
        
        _warehouseRepositoryMock.Setup(x => x.GetWarehouseByIdAsync(warehouseId)).ReturnsAsync(warehouse);
        _warehouseRepositoryMock.Setup(x => x.Update(warehouse));
        _unitOfWorkMock.Setup(x => x.CompleteAsync());

        // Act
        var result = await _warehouseService.UpdateWarehouseAsync(warehouseId, warehouse);

        // Assert
        Assert.NotEqual(Guid.NewGuid(), result.Warehouse.Id);
        Assert.NotEqual(Guid.NewGuid(), result.Warehouse.CountryId);
        Assert.NotEqual(Guid.NewGuid(), result.Warehouse.CityId);
    }
    
    [Fact]
    public async Task Delete_Warehouse_Correct_Test()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var warehouse = new Warehouse(warehouseId) 
        { 
            Address = "Test Address", 
            Capacity = 1000.0, 
            CountryId = Guid.NewGuid(), 
            CityId = Guid.NewGuid() 
        };

        _warehouseRepositoryMock.Setup(x => x.GetWarehouseByIdAsync(warehouseId)).ReturnsAsync(warehouse);
        _warehouseRepositoryMock.Setup(x => x.Remove(warehouse));
        _unitOfWorkMock.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _warehouseService.DeleteWarehouseAsync(warehouseId);

        // Assert
        Assert.Equal(warehouse, result.Warehouse);
    }
    
    [Fact]
    public async Task Delete_Warehouse_Incorrect_Test()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var warehouse = new Warehouse(warehouseId) 
        { 
            Address = "Test Address", 
            Capacity = 1000.0, 
            CountryId = Guid.NewGuid(), 
            CityId = Guid.NewGuid() 
        };

        _warehouseRepositoryMock.Setup(x => x.GetWarehouseByIdAsync(warehouseId)).ReturnsAsync(warehouse);
        _warehouseRepositoryMock.Setup(x => x.Remove(warehouse));
        _unitOfWorkMock.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _warehouseService.DeleteWarehouseAsync(warehouseId);

        // Assert
        Assert.NotEqual(Guid.NewGuid(), result.Warehouse.Id);
        Assert.NotEqual(Guid.NewGuid(), result.Warehouse.CountryId);
        Assert.NotEqual(Guid.NewGuid(), result.Warehouse.CityId);
    }
    
    [Fact]
    public async Task Calculate_Used_Space_Correct_Test()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var warehouse = new Warehouse(warehouseId)
        {
            Address = "Test Address",
            Capacity = 1000.0,
            CountryId = Guid.NewGuid(),
            CityId = Guid.NewGuid()
        };

        var item1 = new Item(Guid.NewGuid()) { Name = "Item 1", Weight = 1.0 };
        var item2 = new Item(Guid.NewGuid()) { Name = "Item 2", Weight = 2.0 };

        var warehouseContents = new List<WarehouseContent>
        {
            new WarehouseContent { WarehouseId = warehouseId, ItemId = item1.Id, Quantity = 10},
            new WarehouseContent { WarehouseId = warehouseId, ItemId = item2.Id, Quantity = 20 }
        };

        _warehouseRepositoryMock.Setup(x => x.GetWarehouseByIdAsync(warehouseId)).ReturnsAsync(warehouse ?? new Warehouse());
        _warehouseContentRepositoryMock.Setup(x => x.GetAllWarehouseContentsAsync()).ReturnsAsync(warehouseContents ?? new List<WarehouseContent>());
        _itemRepositoryMock.Setup(x => x.FindByIdAsync(item1.Id)).ReturnsAsync(item1);
        _itemRepositoryMock.Setup(x => x.FindByIdAsync(item2.Id)).ReturnsAsync(item2);

        // Act
        var result = await _warehouseService.CalculateUsedSpaceAsync(warehouseId);

        // Assert
        Assert.Equal(50.0, result);
        Assert.Equal(warehouseContents[0].Quantity * item1.Weight + warehouseContents[1].Quantity * item2.Weight, result);
        Assert.IsType(typeof(double), result);
    }
    
    [Fact]
    public async Task Calculate_Used_Space_Incorrect_Test()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var warehouse = new Warehouse(warehouseId)
        {
            Address = "Test Address",
            Capacity = 1000.0,
            CountryId = Guid.NewGuid(),
            CityId = Guid.NewGuid()
        };

        var item1 = new Item(Guid.NewGuid()) { Name = "Item 1", Weight = 1.0 };
        var item2 = new Item(Guid.NewGuid()) { Name = "Item 2", Weight = 2.0 };

        var warehouseContents = new List<WarehouseContent>
        {
            new WarehouseContent { WarehouseId = warehouseId, ItemId = item1.Id, Quantity = 10},
            new WarehouseContent { WarehouseId = warehouseId, ItemId = item2.Id, Quantity = 20}
        };

        _warehouseRepositoryMock.Setup(x => x.GetWarehouseByIdAsync(warehouseId)).ReturnsAsync(warehouse ?? new Warehouse());
        _warehouseContentRepositoryMock.Setup(x => x.GetAllWarehouseContentsAsync()).ReturnsAsync(warehouseContents ?? new List<WarehouseContent>());
        _itemRepositoryMock.Setup(x => x.FindByIdAsync(item1.Id)).ReturnsAsync(item1);
        _itemRepositoryMock.Setup(x => x.FindByIdAsync(item2.Id)).ReturnsAsync(item2);

        // Act
        var result = await _warehouseService.CalculateUsedSpaceAsync(warehouseId);

        // Assert
        Assert.NotEqual(100.0, result);
        Assert.IsNotType(typeof(int), result);
    }
}