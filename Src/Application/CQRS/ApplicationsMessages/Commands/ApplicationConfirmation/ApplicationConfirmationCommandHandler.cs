using System;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Communication;
using Domain.Interfaces.Communication.EmailTemplates;
using MediatR;
using WorkingGood.Log;

namespace Application.CQRS.ApplicationsMessages.Commands.ApplicationConfirmation
{
	public class ApplicationConfirmationCommandHandler : INotificationHandler<ApplicationConfirmationCommand>
    {
        private readonly IWgLog<ApplicationConfirmationCommandHandler> _logger;
        private readonly IEmailTemplateDownloader _emailTemplateDownloader;
        private readonly IEmailSender _emailSender;
        
		public ApplicationConfirmationCommandHandler(
            IWgLog<ApplicationConfirmationCommandHandler> logger,
            IEmailTemplateDownloader emailTemplateDownloader,
            IEmailSender emailSender)
        {
            _logger = logger;
            _emailTemplateDownloader = emailTemplateDownloader;
            _emailSender = emailSender;
        }

        public async Task Handle(ApplicationConfirmationCommand notification, CancellationToken cancellationToken)
        {
            _logger.Info($"Handling {nameof(ApplicationConfirmationCommandHandler)}");
            EmailTemplate htmlTemplate =
                await _emailTemplateDownloader.GetByDestination(MessageDestinations.ApplicationConfirmation);
            string offerTitle = string.Empty;
            string messageContent = GetContent(
                htmlTemplate.Content,
                offerTitle,
                notification.FirstName,
                notification.LastName
            );
            _logger.Email(MessageDestinations.ApplicationConfirmation.ToString(), notification.CandidateEmail, notification.OfferId.ToString());
            await _emailSender.Send(
                messageContent,
                "Application confirmation",
                notification.CandidateEmail);
        }
        private string GetContent(string htmlContent, string offerTitle, string firstName, string lastName)
        {
            return htmlContent
                .Replace("[offer_title]", offerTitle)
                .Replace("[first_name]", firstName)
                .Replace("[last_name]", lastName);
        }
    }
}

