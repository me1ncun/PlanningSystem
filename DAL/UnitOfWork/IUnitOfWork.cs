using DAL.Repositories.Interfaces;

namespace DAL.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task SaveChanges();
}