using System;
using Application.CQRS.RegisterMessage.Commands;
using Application.CQRS.RegisterMessage.Commands.ForgotPasswordMessage;
using MediatR;
using Newtonsoft.Json;
using WorkingGood.Log;

namespace WebApi.Services
{
	public class BrokerMessageService : IBrokerMessageService
    {
        private readonly IWgLog<BrokerMessageService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
		public BrokerMessageService(IWgLog<BrokerMessageService> logger, IServiceScopeFactory serviceScopeFactory)
		{
			_logger = logger;
			_serviceScopeFactory = serviceScopeFactory;
		}
        public async Task Handle(string message, string queueName)
        {
			_logger.Info("Handling message from broker");
			IMediator mediator;
			if (queueName == "register")
			{
				INotification registerMessageCommand = JsonConvert.DeserializeObject<RegisterMessageCommand>(message)!;
				using (var scope = _serviceScopeFactory.CreateScope())
				{
					mediator = scope.ServiceProvider.GetService<IMediator>()!;
					await mediator.Publish(registerMessageCommand);
				}
			}
			else if (queueName == "reset_password")
			{
				INotification forgotPasswordCommand = JsonConvert.DeserializeObject<ForgotPasswordCommand>(message)!;
				using (var scope = _serviceScopeFactory.CreateScope())
				{
					mediator = scope.ServiceProvider.GetService<IMediator>()!;
					await mediator.Publish(forgotPasswordCommand);
				}
			}
			else
			{
				_logger.Warn("The queue name is not recognized");
			}
        }
    }
}

