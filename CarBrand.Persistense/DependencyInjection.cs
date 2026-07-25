namespace CarBrand.Persistense;

using CarBrand.Persistense.Data;
using CarBrand.Persistense.Repositories;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services)
    {
        //services.AddSingleton<IUnitOfWork, FakeUnitOfWork>();
        services.AddSingleton<IUnitOfWork, EfUnitOfWork>(); 
        return services;
    }

    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        DbContextOptions options)
    {
        services
            .AddPersistence()
            .AddSingleton<AppDbContext>(
                new AppDbContext((DbContextOptions<AppDbContext>)options));
        return services;
    }
}