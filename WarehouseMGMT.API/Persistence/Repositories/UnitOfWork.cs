using WarehouseMGMT.Domain.Repositories;

namespace WarehouseMGMT.Persistence.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly WarehouseMGMTDbContext _context;
    
    public UnitOfWork(WarehouseMGMTDbContext context)
    {
        _context = context;
    }
    
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}