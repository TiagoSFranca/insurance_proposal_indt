using Microsoft.AspNetCore.Builder;

namespace ContractService.Persistence.Extensions;

public static class DbInitializerExtensions
{
    public static async Task InitializeDatabase(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        using var scope = app.Services.CreateScope();

        var initializer = scope.ServiceProvider.GetRequiredService<ContractInitializer>();

        if (EnvConstants.UseEFMigration())
            await initializer.Initialize();
    }
}
