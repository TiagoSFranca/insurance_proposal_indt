using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProposalService.Application.Validations.Proposal;

namespace ProposalService.Application.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateProposalValidator>();

        services.AddTransient<IProposalService, Services.ProposalService>();

        return services;
    }
}
