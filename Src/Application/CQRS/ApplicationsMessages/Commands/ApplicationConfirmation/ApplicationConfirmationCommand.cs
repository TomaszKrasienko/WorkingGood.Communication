using System;
using MediatR;

namespace Application.CQRS.ApplicationsMessages.Commands.ApplicationConfirmation
{
	public record ApplicationConfirmationCommand : INotification
	{
		public string CandidateEmail { get; init; } = string.Empty;
		public string FirstName { get; init; } = string.Empty;
		public string LastName { get; init; } = string.Empty;
		public Guid OfferId { get; init; }
	}
}

