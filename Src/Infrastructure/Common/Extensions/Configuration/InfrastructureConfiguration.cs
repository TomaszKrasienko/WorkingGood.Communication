using Domain.Interfaces.Communication;
using Domain.Interfaces.Repository;
using Infrastructure.Common.ConfigModels;
using Infrastructure.Communication.Email;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Repositories;
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
        MongoDbConnectionConfig mongoEmailLogConfig = new();
        configuration.Bind("MongoDbConnectionI", mongoEmailLogConfig);
        services.AddSingleton(mongoEmailLogConfig);
        EmailLogConfig emailLogConfig = new();
        configuration.Bind("EmailLogConnection", emailLogConfig);
        services.AddSingleton(emailLogConfig);
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IMongoDbContext, MongoDbContext>();
        services.AddScoped<IEmailLogRepository, EmailLogRepository>();
        return services;
    } 
}