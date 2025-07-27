using Microsoft.AspNetCore.Builder;

namespace ProposalService.Persistence.Extensions;

public static class DbInitializerExtensions
{
    public static async Task InitializeDatabase(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        using var scope = app.Services.CreateScope();

        var initializer = scope.ServiceProvider.GetRequiredService<ProposalInitializer>();

        if (EnvConstants.UseEFMigration())
            await initializer.Initialize();

        if (EnvConstants.SeedDatabase())
            await initializer.Seed();
    }
}
