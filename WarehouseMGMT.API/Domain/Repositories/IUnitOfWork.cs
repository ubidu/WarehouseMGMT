namespace WarehouseMGMT.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}