namespace ProposalService.Persistence.Extensions;

public static class DependencyInjectionExtensions
{
    internal static void SetOptions(this DbContextOptionsBuilder options, string connString)
    {
        options
            .UseNpgsql(connString)
            .UseLowerCaseNamingConvention()
            .UseSnakeCaseNamingConvention();
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        var connString = EnvConstants.DatabaseConnectionString();

        services.AddDbContext<ProposalContext>((provider, options) =>
        {
            options.SetOptions(connString);
        });

        services.AddScoped<IProposalContext, ProposalContext>();

        services.AddScoped<ProposalInitializer>();

        return services;
    }
}
