using Domain.Interfaces.Communication;
using MediatR;

namespace Application.CQRS.RegisterMessage.Commands
{
	public class RegisterMessageCommandHandler : INotificationHandler<RegisterMessageCommand>
	{
		private readonly IEmailSender _emailSender;
		public RegisterMessageCommandHandler(IEmailSender emailSender)
		{
			_emailSender = emailSender;
		}
		public async Task Handle(RegisterMessageCommand notification, CancellationToken cancellationToken)
		{
			await _emailSender.Send($"Your verification token: {notification.RegistrationToken}", "Confirm account",
				notification.Email!);
		}
    }
}

