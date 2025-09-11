using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Interfaces.Persistence;
using TakeServUs.Domain.Common;
using TakeServUs.Persistence.Models;
using TakeServUs.Persistence.Specifications;

namespace TakeServUs.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
  protected readonly IApplicationDbContext _context;
  protected readonly DbSet<T> _dbSet;

  public Repository(IApplicationDbContext context)
  {
    _context = context ?? throw new ArgumentNullException(nameof(context));
    _dbSet = _context.Set<T>();
  }

  #region Read Operations

  public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
  }

  public virtual async Task<T?> GetByIdAsync(
      Guid id,
      Func<IQueryable<T>, IQueryable<T>> includes,
      CancellationToken cancellationToken = default)
  {
    IQueryable<T> query = _dbSet;

    if (includes != null)
    {
      query = includes(query);
    }

    return await query.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
  }

  public virtual async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
  {
    return await _dbSet.ToListAsync(cancellationToken);
  }

  public virtual async Task<IReadOnlyList<T>> GetAsync(
      Expression<Func<T, bool>> predicate,
      CancellationToken cancellationToken = default)
  {
    return await _dbSet
        .Where(predicate)
        .ToListAsync(cancellationToken);
  }

  public virtual async Task<T?> GetFirstOrDefaultAsync(
      Expression<Func<T, bool>> predicate,
      CancellationToken cancellationToken = default)
  {
    return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
  }

  public virtual async Task<T?> GetSingleOrDefaultAsync(
      Expression<Func<T, bool>> predicate,
      CancellationToken cancellationToken = default)
  {
    return await _dbSet.SingleOrDefaultAsync(predicate, cancellationToken);
  }

  #endregion

  #region Advanced Query Operations

  public virtual async Task<IReadOnlyList<T>> GetAsync(
      Expression<Func<T, bool>>? predicate = null,
      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
      Func<IQueryable<T>, IQueryable<T>>? includes = null,
      bool disableTracking = true,
      CancellationToken cancellationToken = default)
  {
    IQueryable<T> query = _dbSet;

    if (disableTracking)
    {
      query = query.AsNoTracking();
    }

    if (includes != null)
    {
      query = includes(query);
    }

    if (predicate != null)
    {
      query = query.Where(predicate);
    }

    if (orderBy != null)
    {
      query = orderBy(query);
    }

    return await query.ToListAsync(cancellationToken);
  }

  public virtual async Task<IPagedResult<T>> GetPagedAsync(
      int pageNumber,
      int pageSize,
      Expression<Func<T, bool>>? predicate = null,
      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
      Func<IQueryable<T>, IQueryable<T>>? includes = null,
      CancellationToken cancellationToken = default)
  {
    if (pageNumber < 1)
      throw new ArgumentException("Page number must be greater than 0", nameof(pageNumber));

    if (pageSize < 1)
      throw new ArgumentException("Page size must be greater than 0", nameof(pageSize));

    IQueryable<T> query = _dbSet.AsNoTracking();

    if (includes != null)
    {
      query = includes(query);
    }

    if (predicate != null)
    {
      query = query.Where(predicate);
    }

    var totalCount = await query.CountAsync(cancellationToken);
    var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

    if (orderBy != null)
    {
      query = orderBy(query);
    }
    else
    {
      query = query.OrderBy(e => e.Id);
    }

    var items = await query
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync(cancellationToken);

    return new PagedResult<T>(items, pageNumber, pageSize, totalCount);
  }

  #endregion

  #region Check and Statistics Operations

  public virtual async Task<bool> ExistsAsync(
      Expression<Func<T, bool>> predicate,
      CancellationToken cancellationToken = default)
  {
    return await _dbSet.AnyAsync(predicate, cancellationToken);
  }

  public virtual async Task<int> CountAsync(
      Expression<Func<T, bool>>? predicate = null,
      CancellationToken cancellationToken = default)
  {
    return predicate == null
        ? await _dbSet.CountAsync(cancellationToken)
        : await _dbSet.CountAsync(predicate, cancellationToken);
  }

  public virtual async Task<long> LongCountAsync(
      Expression<Func<T, bool>>? predicate = null,
      CancellationToken cancellationToken = default)
  {
    return predicate == null
        ? await _dbSet.LongCountAsync(cancellationToken)
        : await _dbSet.LongCountAsync(predicate, cancellationToken);
  }

  #endregion

  #region Write Operations

  public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
  {
    if (entity == null)
      throw new ArgumentNullException(nameof(entity));

    await _dbSet.AddAsync(entity, cancellationToken);
    return entity;
  }

  public virtual async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
  {
    if (entities == null)
      throw new ArgumentNullException(nameof(entities));

    await _dbSet.AddRangeAsync(entities, cancellationToken);
  }

  public virtual T Update(T entity)
  {
    if (entity == null)
      throw new ArgumentNullException(nameof(entity));

    _dbSet.Update(entity);
    return entity;
  }

  public virtual void UpdateRange(IEnumerable<T> entities)
  {
    if (entities == null)
      throw new ArgumentNullException(nameof(entities));

    _dbSet.UpdateRange(entities);
  }

  public virtual void Delete(T entity)
  {
    if (entity == null)
      throw new ArgumentNullException(nameof(entity));

    _dbSet.Remove(entity);
  }

  public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    var entity = await GetByIdAsync(id, cancellationToken);
    if (entity != null)
    {
      Delete(entity);
    }
  }

  public virtual void DeleteRange(IEnumerable<T> entities)
  {
    if (entities == null)
      throw new ArgumentNullException(nameof(entities));

    _dbSet.RemoveRange(entities);
  }

  public virtual async Task<int> DeleteAsync(
      Expression<Func<T, bool>> predicate,
      CancellationToken cancellationToken = default)
  {
    var entities = await _dbSet.Where(predicate).ToListAsync(cancellationToken);

    if (entities.Any())
    {
      _dbSet.RemoveRange(entities);
    }

    return entities.Count;
  }

  public virtual async Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    var entity = await GetByIdAsync(id, cancellationToken);

    if (entity != null && entity is ISoftDelete softDeleteEntity)
    {
      softDeleteEntity.IsDeleted = true;
      softDeleteEntity.DeletedAt = DateTime.UtcNow;

      Update(entity);
    }
  }

  public virtual async Task<int> SoftDeleteAsync(
      Expression<Func<T, bool>> predicate,
      CancellationToken cancellationToken = default)
  {
    var entities = await _dbSet.Where(predicate).ToListAsync(cancellationToken);
    var count = 0;

    foreach (var entity in entities)
    {
      if (entity is ISoftDelete softDeleteEntity)
      {
        softDeleteEntity.IsDeleted = true;
        softDeleteEntity.DeletedAt = DateTime.UtcNow;
        count++;
      }
    }

    if (count > 0)
    {
      _dbSet.UpdateRange(entities);
    }

    return count;
  }

  #endregion

  #region Specification Pattern Support
  public virtual async Task<IReadOnlyList<T>> GetAsync(
      ISpecification<T> specification,
      CancellationToken cancellationToken = default)
  {
    return await ApplySpecification(specification)
        .ToListAsync(cancellationToken);
  }

  public virtual async Task<T?> GetFirstOrDefaultAsync(
      ISpecification<T> specification,
      CancellationToken cancellationToken = default)
  {
    return await ApplySpecification(specification)
        .FirstOrDefaultAsync(cancellationToken);
  }

  protected IQueryable<T> ApplySpecification(ISpecification<T> spec)
  {
    return SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), spec);
  }

  #endregion
}
