namespace Order.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiService(
        this IServiceCollection services
    )
    {
        return services;
    }
    public static WebApplication UseApiService(this WebApplication app)
    {
        return app;
    }
}