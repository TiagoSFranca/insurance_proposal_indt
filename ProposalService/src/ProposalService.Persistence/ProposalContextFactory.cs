using Microsoft.EntityFrameworkCore.Design;
using ProposalService.Persistence.Extensions;

namespace ProposalService.Persistence;

public class ProposalContextFactory : IDesignTimeDbContextFactory<ProposalContext>
{
    public ProposalContext CreateDbContext(string[] args)
    {
        var connString = EnvConstants.DatabaseConnectionString();

        if (string.IsNullOrEmpty(connString))
        {
            throw new InvalidOperationException("Connection string is null or empty.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<ProposalContext>();

        DependencyInjectionExtensions.SetOptions(optionsBuilder, connString);

        return new ProposalContext(optionsBuilder.Options);
    }
}
