using Application.Common.Extensions.Configuration;
using Infrastructure.Common.Extensions.Configuration;
using WebApi.HostedServices;
using WebApi.Services;

namespace WebApi.Common.Extensions.Configuration;

public static class ServicesConfiguration
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddInfrastructureConfiguration(configuration)
            .AddApplicationConfiguration(configuration)
            .ConfigureServices()
            .ConfigureHostedServices();
        return services;
    }
    private static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services
            .AddSingleton<IBrokerMessageService, BrokerMessageService>();
        return services;
    }
    private static IServiceCollection ConfigureHostedServices(this IServiceCollection services)
    {
        services
            .AddHostedService<BrokerService>();
        return services;
    }
}