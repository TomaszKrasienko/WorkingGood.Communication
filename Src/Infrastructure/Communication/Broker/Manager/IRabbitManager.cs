namespace Infrastructure.Communication.Broker.Manager;

public interface IRabbitManager
{
    public Task Send(string message, string exchangeName, string routingKey);
}