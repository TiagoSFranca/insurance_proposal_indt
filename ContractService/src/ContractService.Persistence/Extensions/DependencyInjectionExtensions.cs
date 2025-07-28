using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;

namespace ContractService.Persistence.Extensions;

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
        var proposalAddress = EnvConstants.ProposalServiceAddress();

        services.AddDbContext<ContractContext>((provider, options) =>
        {
            options.SetOptions(connString);
        });

        services
            .AddScoped<IContractContext, ContractContext>()
            .AddScoped<IProposalRepository, ProposalExternalRepository>();

        services.AddHttpClient<IProposalRepository, ProposalExternalRepository>(opt => opt.BaseAddress = new Uri(proposalAddress))
            .AddPolicyHandler(GetRetryPolicy());

        services.AddScoped<ContractInitializer>();

        return services;
    }
    static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 2);

        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(delay);
    }
}
