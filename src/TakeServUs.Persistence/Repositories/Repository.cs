using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Domain.Common;
using TakeServUs.Persistence.DbContexts;

namespace TakeServUs.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
  protected readonly ApplicationDbContext _context;
  protected readonly DbSet<T> _dbSet;

  public Repository(ApplicationDbContext context)
  {
    _context = context;
    _dbSet = context.Set<T>();
  }

  public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
  }

  public virtual async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
  {
    return await _dbSet.ToListAsync(cancellationToken);
  }

  public virtual async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
  {
    return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
  }

  public virtual async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
  {
    return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
  }

  public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
  {
    return await _dbSet.AnyAsync(predicate, cancellationToken);
  }

  public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
  {
    return predicate == null
        ? await _dbSet.CountAsync(cancellationToken)
        : await _dbSet.CountAsync(predicate, cancellationToken);
  }

  public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
  {
    await _dbSet.AddAsync(entity, cancellationToken);
    return entity;
  }

  public virtual async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
  {
    await _dbSet.AddRangeAsync(entities, cancellationToken);
  }

  public virtual void Update(T entity)
  {
    _dbSet.Update(entity);
  }

  public virtual void UpdateRange(IEnumerable<T> entities)
  {
    _dbSet.UpdateRange(entities);
  }

  public virtual void Delete(T entity)
  {
    _dbSet.Remove(entity);
  }

  public virtual void DeleteRange(IEnumerable<T> entities)
  {
    _dbSet.RemoveRange(entities);
  }

  public virtual async Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    var entity = await GetByIdAsync(id, cancellationToken);
    if (entity != null && entity is ISoftDelete softDeletable)
    {
      softDeletable.IsDeleted = true;
      softDeletable.DeletedAt = DateTime.UtcNow;
      Update(entity);
    }
  }
}
