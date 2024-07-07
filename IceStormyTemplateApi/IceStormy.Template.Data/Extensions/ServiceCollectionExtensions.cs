using IceStormy.Template.Common.Extensions;
using IceStormy.Template.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IceStormy.Template.Data.Extensions;

public static class ServiceCollectionExtensions
{
    private const string RepositorySuffix = "Repository";

    /// <summary>
    /// Register all repositories
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var serviceTypes = typeof(BaseRepository<>).Assembly
            .GetTypes()
            .Where(x => x.Name.EndsWith(RepositorySuffix) && x is { IsAbstract: false, IsInterface: false })
            .ToList();

        return services.RegisterImplementations(serviceTypes);
    }

    /// <summary>
    /// Add db context
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="enableSensitiveData"></param>
    /// <returns></returns>
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration, bool enableSensitiveData = false)
    {
        return services.AddDbContextPool<TemplateDbContext>(action =>
        {
            action.UseNpgsql(configuration.GetConnectionString("DatabaseConnectionString"),
                options =>
                {
                    options.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                });

            if (enableSensitiveData)
                action.EnableSensitiveDataLogging();
        });
    }
}