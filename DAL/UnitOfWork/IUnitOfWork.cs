using DAL.Repositories.Interfaces;

namespace DAL.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class;
    Task SaveChanges();
    IEmployeeRepository Employees { get; }
    IRequestRepository Requests { get; }
    IMedicalOpinionRepository MedicalOpinions { get; }
}