using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Erm.DataAccess.Repository;

namespace Erm.DataAccess;

public static class DependencyInjection
{
    public static void RegisterDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ErmDbContext>(options
            => options.UseNpgsql(configuration.GetConnectionString("DbConnection")));

        services.AddStackExchangeRedisCache(options
            => options.Configuration = configuration.GetConnectionString("RedisConnection"));

        services.AddMemoryCache();

        services.AddScoped<BusinessProcessRepository>();
        services.AddScoped<BusinessProcessRepositoryRedisProxy>();
        services.AddScoped<BusinessProcessRepositoryInMemoryProxy>();

        services.AddScoped<RiskProfileRepository>();
        services.AddScoped<RiskProfileRepositoryRedisProxy>();
        services.AddScoped<RiskProfileRepositoryImMemoryProxy>();

        services.AddScoped<RiskRepository>();
        services.AddScoped<RiskRepositoryRedisProxy>();
        services.AddScoped<RiskRepositoryInMemoryProxy>();

        services.AddScoped<NotificationRepository>();
    }
}