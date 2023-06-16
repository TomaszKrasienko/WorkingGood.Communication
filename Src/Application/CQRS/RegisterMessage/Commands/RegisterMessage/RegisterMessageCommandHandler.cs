using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Communication;
using Domain.Interfaces.Communication.Broker;
using Domain.Interfaces.Communication.EmailTemplates;
using Domain.Interfaces.Repository;
using MediatR;
using WorkingGood.Log;

namespace Application.CQRS.RegisterMessage.Commands.RegisterMessage;
public class RegisterMessageCommandHandler : INotificationHandler<RegisterMessageCommand>
{
		private readonly IWgLog<RegisterMessageCommandHandler> _logger;
		private readonly IEmailTemplateDownloader _emailTemplateDownloader;
		private readonly IEmailSender _emailSender;
		private readonly IEmailLogSender _emailLogSender;
		public RegisterMessageCommandHandler(
			IWgLog<RegisterMessageCommandHandler> logger,
			IEmailTemplateDownloader emailTemplateDownloader,
			IEmailSender emailSender,
			IEmailLogSender emailLogSender)
		{
			_logger = logger;
			_emailTemplateDownloader = emailTemplateDownloader;
			_emailSender = emailSender;
			_emailLogSender = emailLogSender;
		}
		public async Task Handle(RegisterMessageCommand notification, CancellationToken cancellationToken)
		{
			_logger.Info($"Handling {nameof(RegisterMessageCommandHandler)}");
			EmailTemplate htmlTemplate = await _emailTemplateDownloader.GetByDestination(MessageDestinations.RegisterEmail);
			string messageContent = GetContent(htmlTemplate.Content, notification.RegistrationUrl!);
			await _emailLogSender.Send(new EmailLog()
			{
				Content = messageContent,
				Type = MessageDestinations.RegisterEmail.ToString(),
				EmailAddress = notification.Email!
			});
			await _emailSender.Send(
				messageContent,
				"Confirm account",
				notification.Email!);
		}

		private string GetContent(string htmlContent, string registrationLink)
		{
			return htmlContent.Replace("[link]", registrationLink);
		}
    }


