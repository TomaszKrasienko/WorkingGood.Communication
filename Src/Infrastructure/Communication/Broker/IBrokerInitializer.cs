using RabbitMQ.Client;

namespace Infrastructure.Communication.Broker;

public interface IBrokerInitializer
{
    ConnectionFactory Initialize();
}