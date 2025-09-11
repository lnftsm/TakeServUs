namespace TakeServUs.Persistence.Models;

using TakeServUs.Application.Interfaces.Persistence;
public class PagedResult<T> : IPagedResult<T>
{
  public PagedResult(IReadOnlyList<T> items, int currentPage, int pageSize, int totalCount)
  {
    Items = items;
    CurrentPage = currentPage;
    PageSize = pageSize;
    TotalCount = totalCount;
    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
  }

  public int CurrentPage { get; }
  public int TotalPages { get; }
  public int PageSize { get; }
  public int TotalCount { get; }
  public bool HasPrevious => CurrentPage > 1;
  public bool HasNext => CurrentPage < TotalPages;
  public IReadOnlyList<T> Items { get; }
}

