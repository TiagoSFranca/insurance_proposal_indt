using Microsoft.EntityFrameworkCore;

namespace InsuranceProposal.UnitTest.Common._Helpers;

public static class DbSetHelpers
{
    public static DbSet<T> EmptyDbSet<T>()
        where T : class
    {
        var items = new List<T>();

        return items.AsDbSet();
    }
}
