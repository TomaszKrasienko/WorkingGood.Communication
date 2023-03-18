using System;
using Application.CQRS.RegisterMessage.Commands;
using MediatR;
using Newtonsoft.Json;

namespace WebApi.Services
{
	public class BrokerMessageService : IBrokerMessageService
    {
        private readonly ILogger<BrokerMessageService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
		public BrokerMessageService(ILogger<BrokerMessageService> logger, IServiceScopeFactory serviceScopeFactory)
		{
			_logger = logger;
			_serviceScopeFactory = serviceScopeFactory;
		}
        public async Task Handle(string message)
        {
			_logger.LogInformation("Handling message from broker");
			IMediator mediator;
	        INotification registerMessageCommand =
		        JsonConvert.DeserializeObject<RegisterMessageCommand>(message)!;			
	        using (var scope = _serviceScopeFactory.CreateScope())
	        {
                mediator = scope.ServiceProvider.GetService<IMediator>()!;
		        await mediator.Publish(registerMessageCommand);
	        }
        }
    }
}

