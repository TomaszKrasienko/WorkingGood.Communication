using Application.Common.Extensions.Configuration;
using Infrastructure.Common.Extensions.Configuration;
using Infrastructure.Communication.Broker;
using WebApi.HostedServices;
using WebApi.Services;
using WorkingGood.Log.Configuration;

namespace WebApi.Common.Extensions.Configuration;

public static class ServicesConfiguration
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddInfrastructureConfiguration(configuration)
            .AddApplicationConfiguration(configuration)
            .ConfigureServices()
            .ConfigureHostedServices()
            .ConfigureSwagger()
            .UseWgLog(configuration, "WorkingGood.Communication");
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