using Microsoft.EntityFrameworkCore;
using MockQueryable.NSubstitute;

namespace InsuranceProposal.UnitTest.Common._Extensions;

public static class DbSetExtensions
{
    public static DbSet<T> AsDbSet<T>(this List<T> items)
        where T : class
    {
        return (items ?? new List<T>())
            .AsQueryable()
            .BuildMockDbSet();
    }
}
