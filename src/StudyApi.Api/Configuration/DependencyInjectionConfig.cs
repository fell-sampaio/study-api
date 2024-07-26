using StudyApi.Business.Interfaces;
using StudyApi.Data.Context;
using StudyApi.Data.Repository;

namespace StudyApi.Api.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        services.AddScoped<AppDbContext>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<IAdressRepository, AdressRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
