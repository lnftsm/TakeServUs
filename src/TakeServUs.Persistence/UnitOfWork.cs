namespace TakeServUs.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TakeServUs.Application.Interfaces.Persistence;
using TakeServUs.Domain.Common;
using TakeServUs.Persistence.DbContexts;
using TakeServUs.Persistence.Repositories;
public class UnitOfWork : IUnitOfWork
{
  private readonly ApplicationDbContext _context;
  private readonly Dictionary<Type, object> _repositories;
  private IDbContextTransaction? _currentTransaction;
  private bool _disposed;

  public UnitOfWork(ApplicationDbContext context)
  {
    _context = context ?? throw new ArgumentNullException(nameof(context));
    _repositories = new Dictionary<Type, object>();
  }

  public bool HasActiveTransaction => _currentTransaction != null;
  public Guid? TransactionId => _currentTransaction?.TransactionId;

  public IRepository<T> Repository<T>() where T : BaseEntity
  {
    var type = typeof(T);
    if (!_repositories.ContainsKey(type))
    {
      var repositoryInstance = new Repository<T>(_context);
      _repositories.Add(type, repositoryInstance);
    }
    return (IRepository<T>)_repositories[type];
  }

  public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    // Auditing is handled by AuditableEntityInterceptor
    return await _context.SaveChangesAsync(cancellationToken);
  }

  public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
  {
    if (_currentTransaction != null)
    {
      throw new InvalidOperationException("A transaction is already in progress.");
    }
    _currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
  }

  public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
  {
    if (_currentTransaction == null)
    {
      throw new InvalidOperationException("No transaction in progress to commit.");
    }

    try
    {
      await SaveChangesAsync(cancellationToken);
      await _currentTransaction.CommitAsync(cancellationToken);
    }
    catch
    {
      await RollbackTransactionAsync(cancellationToken);
      throw;
    }
    finally
    {
      _currentTransaction.Dispose();
      _currentTransaction = null;
    }
  }

  public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
  {
    if (_currentTransaction == null)
    {
      throw new InvalidOperationException("No transaction in progress to rollback.");
    }

    try
    {
      await _currentTransaction.RollbackAsync(cancellationToken);
    }
    finally
    {
      _currentTransaction.Dispose();
      _currentTransaction = null;
    }
  }

  public async Task<TResult> ExecuteInTransactionAsync<TResult>(
      Func<Task<TResult>> operation,
      CancellationToken cancellationToken = default)
  {
    if (HasActiveTransaction)
    {
      return await operation();
    }

    var strategy = _context.Database.CreateExecutionStrategy();
    return await strategy.ExecuteAsync(async () =>
    {
      await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
      try
      {
        var result = await operation();
        await transaction.CommitAsync(cancellationToken);
        return result;
      }
      catch
      {
        await transaction.RollbackAsync(cancellationToken);
        throw;
      }
    });
  }

  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing)
  {
    if (!_disposed)
    {
      if (disposing)
      {
        _currentTransaction?.Dispose();
        _context.Dispose();
      }
      _disposed = true;
    }
  }
}

