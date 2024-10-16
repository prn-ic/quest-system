using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using QuestSystem.Application.Common.Contracts;
using QuestSystem.Infrastructure.Data;
using QuestSystem.Infrastructure.Interceptors;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connection = configuration.GetConnectionString("Default");
        if (string.IsNullOrEmpty(connection))
            throw new InvalidDataException("Строка подключения не найдена");

        services.AddScoped<ISaveChangesInterceptor, DispatchEntitiesInterceptor>();
        services.AddDbContext<AppDbContext>(
            (sp, o) =>
            {
                o.UseNpgsql(connection);
                o.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                o.UseSnakeCaseNamingConvention();
                o.EnableSensitiveDataLogging();
                o.EnableDetailedErrors();
            }
        );

        services.AddScoped<IAppDbContext>(p => p.GetRequiredService<AppDbContext>());
        services.AddScoped<AppDbContextInitializer>();

        return services;
    }

    public static async Task InitializeDatabaseAsync(this IServiceProvider provider)
    {
        using (var scope = provider.CreateScope())
        {
            var initialiser = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
            await initialiser.InitializeAsync();
            await initialiser.SeedAsync();
        }
    }
}
