using Domain.Interfaces.Communication;
using Infrastructure.Common.ConfigModels;
using Infrastructure.Communication.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Common.Extensions.Configuration;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        EmailConfig emailConfig = new();
        configuration.Bind("Email", emailConfig);
        services.AddSingleton(emailConfig);
        RabbitMqConfig rabbitMqConfig = new();
        configuration.Bind("RabbitMq", rabbitMqConfig);
        services.AddSingleton(rabbitMqConfig);
        services.AddScoped<IEmailSender, EmailSender>();
        return services;
    } 
}