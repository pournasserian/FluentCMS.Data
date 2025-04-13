namespace FluentCMS.Data.Common;

/// <summary>
/// Represents a paged result set of items
/// </summary>
/// <typeparam name="T">The type of items in the paged result</typeparam>
public class PagedResult<T>
{
    /// <summary>
    /// Gets or sets the current page number
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Gets or sets the page size
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the total count of items across all pages
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Gets the total number of pages
    /// </summary>
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    /// <summary>
    /// Gets a value indicating whether there is a previous page
    /// </summary>
    public bool HasPrevious => PageNumber > 1;

    /// <summary>
    /// Gets a value indicating whether there is a next page
    /// </summary>
    public bool HasNext => PageNumber < TotalPages;

    /// <summary>
    /// Gets or sets the items on the current page
    /// </summary>
    public IEnumerable<T> Items { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedResult{T}"/> class
    /// </summary>
    public PagedResult()
    {
        Items = new List<T>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedResult{T}"/> class with items
    /// </summary>
    /// <param name="items">The items on the current page</param>
    /// <param name="totalCount">The total count of items across all pages</param>
    /// <param name="pageNumber">The current page number</param>
    /// <param name="pageSize">The page size</param>
    public PagedResult(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
        Items = items;
    }
}