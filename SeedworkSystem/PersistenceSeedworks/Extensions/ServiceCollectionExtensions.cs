using DatabaseContextSeedworks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersistenceSeedworks.LogManager;

namespace PersistenceSeedworks.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLogManagerServices<TDbContext>
        (this IServiceCollection services, string connectionString) where TDbContext : DbContext
    {
        services.AddScoped<ILogServerManager, LogServerManager<TDbContext>>();
        
        services.AddScoped<ILogDetailManager, LogDetailManager<TDbContext>>();

        return services;
    }
}