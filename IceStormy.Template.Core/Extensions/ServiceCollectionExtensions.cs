using IceStormy.Template.Common.Extensions;
using IceStormy.Template.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IceStormy.Template.Core.Extensions;

public static class ServiceCollectionExtensions
{
    private const string ServiceSuffix = "Service";

    /// <summary>
    /// Register all services
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        var serviceTypes = GetAllTypes(typeof(BaseService), ServiceSuffix);
        
        services.RegisterImplementations(serviceTypes);

        return services;
    }

    private static IEnumerable<Type> GetAllTypes(Type baseType, string suffix)
        => baseType.Assembly
            .GetTypes()
            .Where(x => x.Name.EndsWith(suffix) && x is { IsAbstract: false, IsInterface: false })
            .ToList();
}