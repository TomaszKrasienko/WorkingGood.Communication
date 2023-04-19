using Infrastructure.Common.ConfigModels;
using RabbitMQ.Client;

namespace Infrastructure.Communication.Broker;

public class BrokerInitializer : IBrokerInitializer
{
    private readonly RabbitMqConfig _rabbitMqConfig;
    public BrokerInitializer(RabbitMqConfig rabbitMqConfig)
    {
        _rabbitMqConfig = rabbitMqConfig;
    }

    public ConnectionFactory Initialize()
    {
        ConnectionFactory connectionFactory = new ConnectionFactory()
        {
            HostName = _rabbitMqConfig.Host,
            UserName = _rabbitMqConfig.UserName,
            Password = _rabbitMqConfig.Password
        };
        if(_rabbitMqConfig.Port != null) 
            connectionFactory.Port = (int)_rabbitMqConfig.Port;
        return connectionFactory;
    }
}