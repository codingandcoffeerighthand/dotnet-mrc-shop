using Carter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Shared.Exceptions;

namespace Order.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiService(
        this IServiceCollection services
    )
    {
        services.AddCarter();
        services.AddExceptionHandler<CustomExceptionHandler>();
        return services;
    }
    public static WebApplication UseApiService(this WebApplication app)
    {
        app.UseExceptionHandler(opt => { });
        app.MapCarter();
        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        return app;
    }
}