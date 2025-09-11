namespace TakeServUs.Application.Common.Models;

public class PagedResponse<T>
{
  public IReadOnlyList<T> Items { get; set; } = new List<T>();
  public int PageNumber { get; set; }
  public int PageSize { get; set; }
  public int TotalPages { get; set; }
  public int TotalCount { get; set; }
  public bool HasPreviousPage { get; set; }
  public bool HasNextPage { get; set; }

  public PagedResponse() { }

  public PagedResponse(IReadOnlyList<T> items, int pageNumber, int pageSize, int totalCount)
  {
    Items = items;
    PageNumber = pageNumber;
    PageSize = pageSize;
    TotalCount = totalCount;
    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    HasPreviousPage = pageNumber > 1;
    HasNextPage = pageNumber < TotalPages;
  }
}
