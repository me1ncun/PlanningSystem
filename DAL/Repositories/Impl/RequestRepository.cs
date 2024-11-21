using System.Linq.Expressions;
using DAL.Data;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Impl;

public class RequestRepository : BaseRepository<Request>, IRequestRepository
{
    private readonly ApplicationDbContext _context;

    public RequestRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Request?> GetRequestAsync(Expression<Func<Request, bool>> predicate)
    {
        return await _context.Requests.FirstOrDefaultAsync(predicate);
    }
}