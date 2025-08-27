using System.Linq.Expressions;
using TakeServUs.Domain.Common;

namespace TakeServUs.Application.Common.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
  // Read operations
  Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
  Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
  Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
  Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
  Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);

  // Write operations
  Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
  Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
  void Update(T entity);
  void UpdateRange(IEnumerable<T> entities);
  void Delete(T entity);
  void DeleteRange(IEnumerable<T> entities);

  // Soft delete for entities that support it
  Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

public interface IUnitOfWork : IDisposable
{
  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
  Task BeginTransactionAsync(CancellationToken cancellationToken = default);
  Task CommitTransactionAsync(CancellationToken cancellationToken = default);
  Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
