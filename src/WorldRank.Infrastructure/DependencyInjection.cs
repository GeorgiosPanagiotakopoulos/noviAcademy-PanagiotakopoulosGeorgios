using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorldRank.Application.Interfaces;
using WorldRank.Infrastructure.Persistence;
using WorldRank.Infrastructure.Repositories;

namespace WorldRank.Infrastructure;

public static class DependencyInjection
{
    private const string ConnectionString =
        "Server=localhost;Database=WorldRank;Integrated Security=true;TrustServerCertificate=true";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, bool useDatabase)
    {
        if (useDatabase)
        {
            services.AddDbContextFactory<WorldRankDbContext>(options =>
                options.UseSqlServer(ConnectionString));

            services.AddSingleton<IPlayerRepository, DBPlayerRepository>();
            services.AddSingleton<IWalletRepository, DBWalletRepository>();
        }
        else
        {
            services.AddSingleton<IPlayerRepository, InMemoryPlayerRepository>();
            services.AddSingleton<IWalletRepository, InMemoryWalletRepository>();
        }

        return services;
    }
}