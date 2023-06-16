using System;
using Domain.Enums;

namespace Domain.Interfaces.Communication
{
	public interface IEmailSender
	{
		Task Send(string content, string subject, string recipient);
		Task Send(MessageDestinations messageDestinations, string recipients);
	}
}

