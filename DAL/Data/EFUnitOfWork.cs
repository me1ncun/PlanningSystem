using DAL.Repositories.Impl;
using DAL.Repositories.Interfaces;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DAL.Data;

public class EFUnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private Dictionary<Type, object> _repositories;
    private bool disposed = false;

    public EFUnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class
    {
        if (_repositories == null)
        {
            _repositories = new Dictionary<Type, object>();
        }
        
        if (hasCustomRepository)
        {
            var customRepo = _context.GetService<IRepository<TEntity>>();
            if (customRepo != null)
            {
                return customRepo;
            }
        }

        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = new BaseRepository<TEntity>(_context);
        }

        return (IRepository<TEntity>)_repositories[type];
    }
    
    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
    
    public virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _repositories.Clear();

                _context.Dispose();
            }
        }

        disposed = true;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}