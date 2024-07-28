using StudyApi.Business.Interfaces;
using StudyApi.Business.Notifications;
using StudyApi.Business.Services;
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

        services.AddScoped<INotifier, Notifier>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
