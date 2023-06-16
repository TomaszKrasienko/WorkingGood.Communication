

using Domain.Interfaces.Communication;
using Domain.Interfaces.Communication.Broker;
using Domain.Interfaces.Communication.EmailTemplates;
using Infrastructure.Common.ConfigModels;
using Infrastructure.Common.Statics;
using Infrastructure.Communication.Broker;
using Infrastructure.Communication.Broker.Manager;
using Infrastructure.Communication.Email;
using Infrastructure.Communication.EmailTemplates;
using Infrastructure.Persistance;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using Polly;
using RabbitMQ.Client;

namespace Infrastructure.Common.Extensions.Configuration;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .SetServicesConfiguration()
            .SetConfigsConfiguration(configuration)
            .SetHttpClientFactory(configuration);
        return services;
    }
    
    private static IServiceCollection SetServicesConfiguration(this IServiceCollection services)
    {
        return services
            .AddScoped<IEmailSender, EmailSender>()
            .AddScoped<IMongoDbContext, MongoDbContext>()
            .AddSingleton<IPooledObjectPolicy<IModel>, RabbitModelPooledObjectPolicy>()
            .AddScoped<IEmailLogSender, EmailLogSender>()
            .AddScoped<IRabbitManager, RabbitManager>()
            .AddScoped<IEmailTemplateDownloader, EmailTemplateDownloader>();
    }
    
    private static IServiceCollection SetConfigsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        EmailConfig emailConfig = new();
        configuration.Bind("Email", emailConfig);
        services.AddSingleton(emailConfig);
        RabbitMqConfig rabbitMqConfig = new();
        configuration.Bind("RabbitMq", rabbitMqConfig);
        services.AddSingleton(rabbitMqConfig);
        MongoDbConnectionConfig mongoEmailLogConfig = new();
        configuration.Bind("MongoDbConnection", mongoEmailLogConfig);
        services.AddSingleton(mongoEmailLogConfig);
        return services;
    }

    private static IServiceCollection SetHttpClientFactory(this IServiceCollection services, IConfiguration configuration)
    {
        var retryPolicy = Policy.HandleResult<HttpResponseMessage>(
                r => !r.IsSuccessStatusCode)
            .RetryAsync(3);
        //Todo: Przerobić na configuration exception -> coś takiego 
        string toolServiceAddress = configuration.GetValue<string>("WorkingGoodToolAddress") ?? throw new Exception();
        services.AddHttpClient(HttpClients.WgTool, client =>
        {
            client.BaseAddress = new Uri(toolServiceAddress);
            client.Timeout = new TimeSpan(0, 0, 30);
            client.DefaultRequestHeaders.Clear();
        }).AddPolicyHandler(retryPolicy);
        return services;
    }
}