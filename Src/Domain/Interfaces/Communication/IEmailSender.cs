using System;
namespace Domain.Interfaces.Communication
{
	public interface IEmailSender
	{
		Task Send(string content, string subject, string recipient);
	}
}

