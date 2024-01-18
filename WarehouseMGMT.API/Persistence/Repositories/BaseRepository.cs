namespace WarehouseMGMT.Persistence.Repository;

public abstract class BaseRepository
{
    protected readonly WarehouseMGMTDbContext _context;
    
    public BaseRepository(WarehouseMGMTDbContext context)
    {
        _context = context;
    }
}