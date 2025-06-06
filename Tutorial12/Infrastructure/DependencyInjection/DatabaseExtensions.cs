using Microsoft.EntityFrameworkCore;
using Tutorial12.Infrastructure.Persistence;

namespace Tutorial12.Infrastructure.DependencyInjection;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
}