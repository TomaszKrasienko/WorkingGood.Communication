using System;
using MediatR;

namespace Application.CQRS.RegisterMessage.Commands
{
	public record RegisterMessageCommand : INotification
	{
		public string? Email { get; init; }
		public string? FirstName { get; init; }
		public string? LastName { get; init; }
		public string? RegistrationUrl { get; init; }
	}
}

