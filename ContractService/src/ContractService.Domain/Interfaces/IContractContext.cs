using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ContractService.Domain.Interfaces;

public interface IContractContext
{
    DatabaseFacade Database { get; }

    DbSet<Contract> Contracts { get; set; }

    DbSet<TEntity> Set<TEntity>()
        where TEntity : DbEntity;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
