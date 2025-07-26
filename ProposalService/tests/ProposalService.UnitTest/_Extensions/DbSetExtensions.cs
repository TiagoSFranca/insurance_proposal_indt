using Microsoft.EntityFrameworkCore;
using MockQueryable.NSubstitute;
using ProposalService.Domain.Entities.Base;

namespace ProposalService.UnitTest._Extensions;

public static class DbSetExtensions
{
    public static DbSet<T> AsDbSet<T>(this List<T> items)
        where T : DbEntity
    {
        return (items ?? new List<T>())
            .AsQueryable()
            .BuildMockDbSet();
    }
}
