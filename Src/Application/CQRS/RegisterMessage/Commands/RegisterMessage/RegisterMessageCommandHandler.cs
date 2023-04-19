using Domain.Entities;
using Domain.Interfaces.Communication;
using Domain.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.RegisterMessage.Commands.RegisterMessage;
public class RegisterMessageCommandHandler : INotificationHandler<RegisterMessageCommand>
{
		private readonly ILogger<RegisterMessageCommandHandler> _logger;
		private readonly IEmailSender _emailSender;
		private readonly IEmailLogRepository _emailLogRepository;
		public RegisterMessageCommandHandler(ILogger<RegisterMessageCommandHandler> logger, IEmailSender emailSender, IEmailLogRepository emailLogRepository)
		{
			_logger = logger;
			_emailSender = emailSender;
			_emailLogRepository = emailLogRepository;
		}
		public async Task Handle(RegisterMessageCommand notification, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Handling RegisterMessageCommand");
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


