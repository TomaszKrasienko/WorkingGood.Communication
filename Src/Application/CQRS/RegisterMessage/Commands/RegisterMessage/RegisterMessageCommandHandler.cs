using Domain.Entities;
using Domain.Interfaces.Communication;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.CQRS.RegisterMessage.Commands.RegisterMessage;
public class RegisterMessageCommandHandler : INotificationHandler<RegisterMessageCommand>
	{
		private readonly IEmailSender _emailSender;
		private readonly IEmailLogRepository _emailLogRepository;
		public RegisterMessageCommandHandler(IEmailSender emailSender, IEmailLogRepository emailLogRepository)
		{
			_emailSender = emailSender;
			_emailLogRepository = emailLogRepository;
		}
		public async Task Handle(RegisterMessageCommand notification, CancellationToken cancellationToken)
		{
			await _emailLogRepository.AddLog(new EmailLog
			{
				Content = $"Your verification token: {notification.RegistrationToken}",
				Type = "Register",
				EmailAddress = notification.Email!
			});
			await _emailSender.Send(
				$"Your verification token: {notification.RegistrationToken}",
				"Confirm account",
				notification.Email!);
		}
    }


