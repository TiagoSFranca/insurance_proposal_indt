using ContractService.Persistence.Extensions;
using Microsoft.EntityFrameworkCore.Design;

namespace ContractService.Persistence;

public class ContractContextFactory : IDesignTimeDbContextFactory<ContractContext>
{
    public ContractContext CreateDbContext(string[] args)
    {
        var connString = EnvConstants.DatabaseConnectionString();

        if (string.IsNullOrEmpty(connString))
        {
            throw new InvalidOperationException("Connection string is null or empty.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<ContractContext>();

        DependencyInjectionExtensions.SetOptions(optionsBuilder, connString);

        return new ContractContext(optionsBuilder.Options);
    }
}
