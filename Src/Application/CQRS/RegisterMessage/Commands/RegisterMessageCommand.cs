using System;
using MediatR;

namespace Application.CQRS.RegisterMessage.Commands
{
	public class RegisterMessageCommand : INotification
	{
		public string? Email { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? RegistrationToken { get; set; }
	}
}

