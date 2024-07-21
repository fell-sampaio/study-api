using Microsoft.EntityFrameworkCore;
using StudyApi.Business.Interfaces;
using StudyApi.Business.Models;
using StudyApi.Data.Context;
using System.Linq.Expressions;

namespace StudyApi.Data.Repository;
public class Repository<TEntity>(AppDbContext dbContext) : IRepository<TEntity> where TEntity : Entity, new()
{
    protected readonly AppDbContext _dbContext = dbContext;
    protected readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

    public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public virtual async Task<TEntity> GetById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<List<TEntity>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task Add(TEntity entity)
    {
        _dbSet.Add(entity);
        await SaveChanges();
    }

    public virtual async Task Update(TEntity entity)
    {
        _dbSet.Update(entity);
        await SaveChanges();
    }

    public virtual async Task Delete(Guid id)
    {
        _dbSet.Remove(new TEntity { Id = id });
        await SaveChanges();
    }

    public async Task<int> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
        GC.SuppressFinalize(this);
    }
}
