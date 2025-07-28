using InsuranceProposal.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace InsuranceProposal.Common.Data;

public interface IDbContext
{
    DatabaseFacade Database { get; }

    DbSet<TEntity> Set<TEntity>()
        where TEntity : DbEntity;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
