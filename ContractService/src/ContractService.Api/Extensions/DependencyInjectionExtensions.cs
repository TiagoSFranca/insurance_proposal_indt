using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using ContractService.Persistence;

namespace ContractService.Api.Extensions;

public static class DependencyInjectionExtensions
{
    private const string TAG_READY = "ready";

    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddHealthChecks()
            .AddDbContextCheck<ContractContext>("db", tags: [TAG_READY]);

        return services;
    }

    public static WebApplication UseHealthChecksEndpoints(this WebApplication app)
    {
        app.MapHealthChecks("/healthz/ready", new HealthCheckOptions
        {
            Predicate = healthCheck => healthCheck.Tags.Contains(TAG_READY),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.MapHealthChecks("/healthz/live", new HealthCheckOptions
        {
            Predicate = _ => false,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        return app;
    }
}
