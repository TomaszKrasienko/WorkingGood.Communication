using System;
using System.Text;
using Infrastructure.Common.ConfigModels;
using Infrastructure.Communication.Broker;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WebApi.Services;

namespace WebApi.HostedServices
{
	public class BrokerService : BackgroundService
	{
        private readonly RabbitMqConfig _rabbitMqConfig;
        private readonly IPooledObjectPolicy<IModel> _pooledObjectPolicy;
        private IBrokerMessageService? _brokerMessageService;
        private IConnection? _connection;
        private IModel? _channel;
		public BrokerService(IServiceScopeFactory scopeFactory, RabbitMqConfig rabbitMqConfig, IPooledObjectPolicy<IModel> pooledObjectPolicy)
		{
            _rabbitMqConfig = rabbitMqConfig;
            _pooledObjectPolicy = pooledObjectPolicy;
            InitializeServices(scopeFactory);
            InitializeConnection();
		}
        private void InitializeServices(IServiceScopeFactory scopeFactory)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                _brokerMessageService = scope.ServiceProvider.GetRequiredService<IBrokerMessageService>();
            }
        }
        private void InitializeConnection()
        {
            _channel = _pooledObjectPolicy.Create();
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async(ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.Span);
                await HandleMessage(content, ea.RoutingKey);
                _channel!.BasicAck(ea.DeliveryTag, false);
            };
            foreach (var route in _rabbitMqConfig.ReceivingRoutes)
            {
                _channel.BasicConsume(route.Queue, false, consumer);
            }
            return Task.CompletedTask;
        }
        private async Task HandleMessage(string content, string routingKey)
        {
            await _brokerMessageService!.Handle(content, routingKey);
        }
    }
}

