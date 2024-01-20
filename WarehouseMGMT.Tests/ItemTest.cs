using Moq;
using WarehouseMGMT.Domain.Repositories;
using WarehouseMGMT.Models;
using WarehouseMGMT.Services;

namespace WarehouseMGMT.Tests;

public class ItemTest
{
    private readonly IItemService _itemService;
    private readonly Mock<IItemRepository> _itemRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public ItemTest()
    {
        _itemRepositoryMock = new Mock<IItemRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _itemService = new ItemService(_itemRepositoryMock.Object, _unitOfWorkMock.Object);
    }
    
    [Fact]
    public async Task Get_Item_By_Id_Correct_Test()
    {
        // Arrange
        var existingItemId = Guid.NewGuid();
        var existingItem = new Item(existingItemId) { Name = "Existing Item", Weight = 1.0 };

        _itemRepositoryMock.Setup(x => x.FindByIdAsync(existingItemId)).ReturnsAsync(existingItem);

        // Act
        var result = await _itemService.GetItemByIdAsync(existingItemId);

        // Assert
        Assert.Equal(existingItem.Name, result.Name);
        Assert.Equal(existingItem.Weight, result.Weight);
    }
    
    [Fact]
    public async Task Get_Item_By_Id_Incorrect_Test()
    {
        // Arrange
        var existingItemId = Guid.NewGuid();
        var existingItem = new Item(existingItemId) { Name = "Existing Item", Weight = 1.0 };

        _itemRepositoryMock.Setup(x => x.FindByIdAsync(existingItemId)).ReturnsAsync(existingItem);

        // Act
        var result = await _itemService.GetItemByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Theory]
    [InlineData("Test Item", 1.0)]
    [InlineData("Test Item 3", 30000.0)]
    public void Add_Item_Correct_Test(string name, double weight)
    {
        // Arrange
        var item = new Item
        {
            Name = name,
            Weight = weight
        };
        
        // Act
        var result = _itemService.SaveAsync(item).Result;
        
        // Assert
        Assert.True(result.Success);
    }
    
    [Theory]
    [InlineData("Test Item", -1.0)]
    [InlineData("Test Item 2", -2.0)]
    public void Add_Item_Incorrect_Test(string name, double weight)
    {
        // Arrange
        var item = new Item
        {
            Name = name,
            Weight = weight
        };
        
        // Act
        var result = _itemService.SaveAsync(item).Result;
        
        // Assert
        Assert.False(result.Success);
    }
    
    [Fact]
    public async Task Update_Item_Correct_Test()
    {
        // Arrange
        var existingItemId = Guid.NewGuid();
        var existingItem = new Item(existingItemId) { Name = "Existing Item", Weight = 1.0 };
        var updatedItem = new Item { Name = "Updated Item", Weight = 2.0 };

        _itemRepositoryMock.Setup(x => x.FindByIdAsync(existingItemId)).ReturnsAsync(existingItem);
        _itemRepositoryMock.Setup(x => x.Update(existingItem));
        _unitOfWorkMock.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _itemService.UpdateAsync(existingItemId, updatedItem);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(updatedItem.Name, result.Item.Name);
        Assert.Equal(updatedItem.Weight, result.Item.Weight);
    }
    
    [Fact]
    public async Task Update_Item_Incorrect_Test()
    {
        // Arrange
        var existingItemId = Guid.NewGuid();
        var existingItem = new Item(existingItemId) { Name = "Existing Item", Weight = 1.0 };
        var updatedItem = new Item { Name = "Updated Item", Weight = -2.0 };

        _itemRepositoryMock.Setup(x => x.FindByIdAsync(existingItemId)).ReturnsAsync(existingItem);
        _itemRepositoryMock.Setup(x => x.Update(existingItem));
        _unitOfWorkMock.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _itemService.UpdateAsync(existingItemId, updatedItem);

        // Assert
        Assert.False(result.Success);
    }
    
    [Fact]
    public async Task Delete_Item_Correct_Test()
    {
        // Arrange
        var existingItemId = Guid.NewGuid();
        var existingItem = new Item(existingItemId) { Name = "Existing Item", Weight = 1.0 };

        _itemRepositoryMock.Setup(x => x.FindByIdAsync(existingItemId)).ReturnsAsync(existingItem);
        _itemRepositoryMock.Setup(x => x.Remove(existingItem));
        _unitOfWorkMock.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _itemService.DeleteAsync(existingItemId);

        // Assert
        Assert.True(result.Success);
    }
    
    [Fact]
    public async Task Delete_Item_Incorrect_Test()
    {
        // Arrange
        var existingItemId = Guid.NewGuid();
        var existingItem = new Item(existingItemId) { Name = "Existing Item", Weight = 1.0 };

        _itemRepositoryMock.Setup(x => x.FindByIdAsync(existingItemId)).ReturnsAsync(existingItem);
        _itemRepositoryMock.Setup(x => x.Remove(existingItem));
        _unitOfWorkMock.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _itemService.DeleteAsync(Guid.NewGuid());

        // Assert
        Assert.False(result.Success);
    }
    
}