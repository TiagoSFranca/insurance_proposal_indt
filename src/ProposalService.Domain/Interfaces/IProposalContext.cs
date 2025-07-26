using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ProposalService.Domain.Interfaces;

public interface IProposalContext
{
    DatabaseFacade Database { get; }

    DbSet<Proposal> Proposals { get; set; }
    DbSet<ProposalStatus> ProposalStatuses { get; set; }

    DbSet<TEntity> Set<TEntity>()
        where TEntity : DbEntity;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
