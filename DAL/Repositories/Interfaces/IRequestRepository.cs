using System.Linq.Expressions;
using DAL.Entities;

namespace DAL.Repositories.Interfaces;

public interface IRequestRepository : IRepository<Request>
{
    Task<Request?> GetRequestAsync(Expression<Func<Request, bool>> predicate);
}