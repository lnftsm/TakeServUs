namespace TakeServUs.Application.Interfaces.Persistence;

using System.Linq.Expressions;
using TakeServUs.Domain.Common;
public interface IRepository<T> where T : BaseEntity
{
  #region Basic CRUD Operations
  Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<T?> GetByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>> includes, CancellationToken cancellationToken = default);
  Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
  Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
  Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
  Task<T?> GetSingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

  #endregion

  #region Advanced Query Operations
  Task<IReadOnlyList<T>> GetAsync(
      Expression<Func<T, bool>>? predicate = null,
      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
      Func<IQueryable<T>, IQueryable<T>>? includes = null,
      bool disableTracking = true,
      CancellationToken cancellationToken = default);
  Task<IPagedResult<T>> GetPagedAsync(
      int pageNumber,
      int pageSize,
      Expression<Func<T, bool>>? predicate = null,
      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
      Func<IQueryable<T>, IQueryable<T>>? includes = null,
      CancellationToken cancellationToken = default);

  #endregion

  #region Control and Statistics Operations
  Task<bool> ExistsAsync(
      Expression<Func<T, bool>> predicate,
      CancellationToken cancellationToken = default);
  Task<int> CountAsync(
      Expression<Func<T, bool>>? predicate = null,
      CancellationToken cancellationToken = default);
  Task<long> LongCountAsync(
      Expression<Func<T, bool>>? predicate = null,
      CancellationToken cancellationToken = default);

  #endregion

  #region Write Operations
  Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
  Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
  T Update(T entity);
  void UpdateRange(IEnumerable<T> entities);
  void Delete(T entity);
  Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
  void DeleteRange(IEnumerable<T> entities);
  Task<int> DeleteAsync(
      Expression<Func<T, bool>> predicate,
      CancellationToken cancellationToken = default);
  Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default);
  Task<int> SoftDeleteAsync(
      Expression<Func<T, bool>> predicate,
      CancellationToken cancellationToken = default);

  #endregion

  #region Specification Pattern Operations
  Task<IReadOnlyList<T>> GetAsync(
      ISpecification<T> specification,
      CancellationToken cancellationToken = default);
  Task<T?> GetFirstOrDefaultAsync(
      ISpecification<T> specification,
      CancellationToken cancellationToken = default);

  #endregion
}

#region Supporting Interfaces

public interface ISpecification<T>
{
  Expression<Func<T, bool>>? Criteria { get; }
  List<Expression<Func<T, object>>> Includes { get; }
  List<string> IncludeStrings { get; }
  Expression<Func<T, object>>? OrderBy { get; }
  Expression<Func<T, object>>? OrderByDescending { get; }
  Expression<Func<T, object>>? GroupBy { get; }
  int Take { get; }
  int Skip { get; }
  bool IsPagingEnabled { get; }
}

#endregion
