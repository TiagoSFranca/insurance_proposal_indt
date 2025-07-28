namespace ContractService.Application.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IContractService, Services.ContractService>();

        return services;
    }
}
