using Microsoft.EntityFrameworkCore;
using ProposalService.Domain.Entities.Base;

namespace ProposalService.UnitTest._Helpers;

public static class DbSetHelpers
{
    public static DbSet<T> EmptyDbSet<T>()
        where T : DbEntity
    {
        var items = new List<T>();

        return items.AsDbSet();
    }
}
