using Help.Desk.Domain.IRepositories.Common;
using Help.Desk.Infrastructure.Database.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Help.Desk.Infrastructure.Database.EntityFramework.Repositories.Common;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly HelpDeskDbContext _context;
    private readonly DbSet<TEntity> _dbSet;
    
    
    public GenericRepository(HelpDeskDbContext context) 
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        var result = await _dbSet.AddAsync(entity);
        return result.Entity;
    }

    public virtual Task<List<TEntity>> GetAllAsync()
    {
        return _dbSet.ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entityEntry = _dbSet.Update(entity);
        return entityEntry.Entity;
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            return false; // La entidad no existe
        }
        _dbSet.Remove(entity);
        return true;
    }
}