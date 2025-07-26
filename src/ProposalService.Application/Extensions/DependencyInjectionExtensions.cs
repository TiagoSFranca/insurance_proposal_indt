using Microsoft.Extensions.DependencyInjection;

namespace ProposalService.Application.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IProposalService, Services.ProposalService>();

        return services;
    }
}
