namespace TakeServUs.Application.Interfaces.Persistence;

using TakeServUs.Domain.Common;
public interface IUnitOfWork : IDisposable
{
  IRepository<T> Repository<T>() where T : BaseEntity;
  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
  Task BeginTransactionAsync(CancellationToken cancellationToken = default);
  Task CommitTransactionAsync(CancellationToken cancellationToken = default);
  Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
  Task<TResult> ExecuteInTransactionAsync<TResult>(
       Func<Task<TResult>> operation,
       CancellationToken cancellationToken = default);

  bool HasActiveTransaction { get; }

  Guid? TransactionId { get; }
}
