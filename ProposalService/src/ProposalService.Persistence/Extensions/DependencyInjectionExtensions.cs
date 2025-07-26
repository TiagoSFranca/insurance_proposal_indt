namespace ProposalService.Persistence.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        var connString = EnvConstants.DatabaseConnectionString();

        services.AddDbContext<ProposalContext>((provider, options) =>
        {
            options
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            //.UseNpgsql(connString)
            //.UseLowerCaseNamingConvention()
            //.UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IProposalContext, ProposalContext>();

        return services;
    }
}
