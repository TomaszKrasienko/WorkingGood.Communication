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
            .AddSingleton<IBrokerMessageService, BrokerMessageService>()
            .AddHostedService<BrokerService>();
        return services;
    }
}