using Tutorial12.Domain.Interfaces;
using Tutorial12.Infrastructure.Repositories;

namespace Tutorial12.Infrastructure.DependencyInjection;

public static class ScopedServicesExtensions
{
    public static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        services.AddScoped<ITripRepository, TripRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        return services;
    }
}