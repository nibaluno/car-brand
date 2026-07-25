using Microsoft.Extensions.DependencyInjection;

namespace CarBrand.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // регистрируем MediatR
        services.AddMediatR(conf =>
            conf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        
        return services;
    }
}