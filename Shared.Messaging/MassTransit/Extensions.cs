using System.ComponentModel.Design;
using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Messaging.MassTransit;

public static class Extensions
{
    public static IServiceCollection AddMessageBroker(
        this IServiceCollection services,
        IConfiguration config,
        Assembly? assembly = null
        )
    {
        services.AddMassTransit(cfg =>
        {
            cfg.SetKebabCaseEndpointNameFormatter();
            if (assembly != null)
            {
                cfg.AddConsumers(assembly);
            }
            cfg.UsingRabbitMq((context, configu) =>
            {
                configu.Host(new Uri(config["MessageBrokder:Host"]!), h =>
                {
                    h.Username(config["MessageBrokder:User"]!);
                    h.Password(config["MessageBrokder:Password"]!);
                });
                configu.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
