namespace ContractService.Domain.Responses;

public class PageResponse<T>
{
    public List<T> Content { get; private set; }
    public Page Page { get; set; }

    private PageResponse() { }

    public PageResponse(List<T> content, PageRequest pageRequest, long totalResult)
    {
        ArgumentNullException.ThrowIfNull(pageRequest);

        content ??= new List<T>();

        Content = content;

        var page = new Page
        {
            Number = pageRequest.Number,
            Limit = pageRequest.Limit,
            Total = (int)totalResult
        };

        page.TotalPages = (int)Math.Ceiling(totalResult / (decimal)page.Limit);
        page.First = page.Number == 1;
        page.Last = page.Number >= page.TotalPages;

        Page = page;
    }

    public static PageResponse<T> For(List<T> content, PageRequest pageRequest, long totalResult) => new(content, pageRequest, totalResult);
}

public class Page
{
    public int Number { get; set; }
    public int Limit { get; set; }
    public int Total { get; set; }
    public int TotalPages { get; set; }
    public bool First { get; set; }
    public bool Last { get; set; }
}
