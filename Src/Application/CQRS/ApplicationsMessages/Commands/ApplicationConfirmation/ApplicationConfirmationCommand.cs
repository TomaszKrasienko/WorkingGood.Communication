using System;
using MediatR;

namespace Application.CQRS.ApplicationsMessages.Commands.ApplicationConfirmation
{
	public record ApplicationConfirmationCommand : INotification
	{
		public string CandidateEmail { get; init; } = string.Empty;
		public string CandidateFirstName { get; init; } = string.Empty;
		public string CandidateLastName { get; init; } = string.Empty;
		public Guid OfferId { get; init; }
	}
}

