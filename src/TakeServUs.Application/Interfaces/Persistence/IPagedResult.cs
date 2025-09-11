namespace TakeServUs.Application.Interfaces.Persistence;

using Microsoft.EntityFrameworkCore;
public interface IPagedResult<T>
{
  int CurrentPage { get; }
  int TotalPages { get; }
  int PageSize { get; }
  int TotalCount { get; }
  bool HasPrevious { get; }
  bool HasNext { get; }
  IReadOnlyList<T> Items { get; }
}

public class PagedList<T> : List<T>, IPagedResult<T>
{
  public PagedList(List<T> items, int count, int pageNumber, int pageSize)
  {
    TotalCount = count;
    PageSize = pageSize;
    CurrentPage = pageNumber;
    TotalPages = (int)Math.Ceiling(count / (double)pageSize);

    AddRange(items);
  }

  public int CurrentPage { get; }
  public int TotalPages { get; }
  public int PageSize { get; }
  public int TotalCount { get; }
  public bool HasPrevious => CurrentPage > 1;
  public bool HasNext => CurrentPage < TotalPages;
  public bool IsFirstPage => CurrentPage == 1;
  public bool IsLastPage => CurrentPage == TotalPages;

  public IReadOnlyList<T> Items => this;

  public int FirstItemIndex => TotalCount > 0 ? ((CurrentPage - 1) * PageSize) + 1 : 0;
  public int LastItemIndex => TotalCount > 0 ? Math.Min(CurrentPage * PageSize, TotalCount) : 0;

  public static async Task<PagedList<T>> CreateAsync(
      IQueryable<T> source,
      int pageNumber,
      int pageSize)
  {
    var count = await source.CountAsync();
    var items = await source
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    return new PagedList<T>(items, count, pageNumber, pageSize);
  }
}
