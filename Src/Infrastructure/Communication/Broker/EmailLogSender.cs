using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Interfaces.Communication.Broker;
using Infrastructure.Common.ConfigModels;
using Infrastructure.Communication.Broker.Manager;
using Infrastructure.Persistance.Repositories;
using Newtonsoft.Json;
using WorkingGood.Log;

namespace Infrastructure.Communication.Broker;

public class EmailLogSender : IEmailLogSender
{
    private readonly IWgLog<EmailLogSender> _logger;
    private readonly IRabbitManager _rabbitManager;
    private readonly RabbitMqConfig _rabbitMqConfig;
    public EmailLogSender(IWgLog<EmailLogSender> logger, IRabbitManager rabbitManager, RabbitMqConfig rabbitMqConfig)
    {
        _logger = logger;
        _rabbitManager = rabbitManager;
        _rabbitMqConfig = rabbitMqConfig;
    }
    public Task Send(EmailLog emailLog)
    {
        _logger.Info($"Executing {nameof(Send)} in class {nameof(EmailLogSender)}");
        string jsonMessage = JsonConvert.SerializeObject(emailLog);
        RabbitMqRouteConfig emailLogRoute = _rabbitMqConfig
            .SendingRoutes
            .FirstOrDefault(x => x.Destination == "EmailLog")!;
        _rabbitManager.Send(jsonMessage, emailLogRoute.Exchange, emailLogRoute.Queue);
        return Task.CompletedTask; 
    }
}