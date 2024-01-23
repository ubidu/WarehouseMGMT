using Microsoft.EntityFrameworkCore;
using WarehouseMGMT.Domain.Repositories;
using WarehouseMGMT.Models;
using WarehouseMGMT.Persistence;
using WarehouseMGMT.Persistence.Repository;

namespace WarehouseMGMT.Tests;

public class ItemRepositoryTest
{
    private WarehouseMGMTDbContext _context;
    private IItemRepository _itemRepository;
    
    public ItemRepositoryTest()
    {
        var options = new DbContextOptionsBuilder<WarehouseMGMTDbContext>();
        options.UseNpgsql(
            "Host=localhost; Database=WarehouseManagement; Username=postgres; Password=admin; Include Error Detail=true");
        _context = new WarehouseMGMTDbContext(options.Options);
        _itemRepository = new ItemRepository(_context);
    }
    
    [Fact]
    public async Task Add_Item_Correct_Test()
    {
        // Arrange
        var item = new Item { Name = "Test Item", Weight = 1.0 };
        
        // Act
        await _itemRepository.Add(item);
        await _context.SaveChangesAsync();
        
        // Assert
        Assert.NotNull(await _itemRepository.FindByIdAsync(item.Id));
        
        // Clean up
        _context.Remove(item);
        await _context.SaveChangesAsync();
    }
}