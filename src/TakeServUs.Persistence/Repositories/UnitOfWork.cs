using Microsoft.EntityFrameworkCore.Storage;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Persistence.DbContexts;

namespace TakeServUs.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
  private readonly ApplicationDbContext _context;
  private IDbContextTransaction? _currentTransaction;

  public UnitOfWork(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    return await _context.SaveChangesAsync(cancellationToken);
  }

  public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
  {
    _currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
  }

  public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
  {
    if (_currentTransaction != null)
    {
      await _currentTransaction.CommitAsync(cancellationToken);
      _currentTransaction.Dispose();
      _currentTransaction = null;
    }
  }

  public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
  {
    if (_currentTransaction != null)
    {
      await _currentTransaction.RollbackAsync(cancellationToken);
      _currentTransaction.Dispose();
      _currentTransaction = null;
    }
  }

  public void Dispose()
  {
    _currentTransaction?.Dispose();
    _context.Dispose();
  }
}
