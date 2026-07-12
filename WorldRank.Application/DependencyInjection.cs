using Microsoft.Extensions.DependencyInjection;
using WorldRank.Application.Strategies;

namespace WorldRank.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IFundsStrategy, AddFundsStrategy>();
        services.AddSingleton<IFundsStrategy, SubstractFundsStrategy>();
        services.AddSingleton<IFundsStrategy, ForceSubstractFundsStrategy>();
        return services;
    }
}