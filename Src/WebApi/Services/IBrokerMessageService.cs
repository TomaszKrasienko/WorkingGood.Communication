
using System;
namespace WebApi.Services
{
	public interface IBrokerMessageService
	{
		Task Handle(string message, string queueName);
	}
}

