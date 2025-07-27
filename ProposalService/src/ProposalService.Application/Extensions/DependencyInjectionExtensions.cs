using FluentValidation;
using ProposalService.Application.Services;
using ProposalService.Application.Validations.Proposal;

namespace ProposalService.Application.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateProposalValidator>();

        services.AddTransient<IProposalService, Services.ProposalService>();
        services.AddTransient<IInsuranceTypeService, InsuranceTypeService>();
        services.AddTransient<IProposalStatusService, ProposalStatusService>();
        services.AddTransient<IPaymentMethodService, PaymentMethodService>();

        return services;
    }
}
