namespace ProposalService.Domain.Requests;

public class PageRequest
{
    private const int DefaultPageLimit = 10;

    private int number;
    private int limit;
    public int Number { get { return number; } }
    public int Limit { get { return limit; } }

    private PageRequest() { }

    public static PageRequest Of(int? number, int? limit)
    {
        var n = number ?? 1;
        var l = limit ?? DefaultPageLimit;

        return Of(n, l);
    }

    public static PageRequest Of(int number, int limit)
    {
        var pr = new PageRequest
        {
            number = number < 1 ? 1 : number,
            limit = limit < 1 ? 1 : limit
        };

        return pr;

    }

    public static PageRequest First() => Of(1, DefaultPageLimit);
}
