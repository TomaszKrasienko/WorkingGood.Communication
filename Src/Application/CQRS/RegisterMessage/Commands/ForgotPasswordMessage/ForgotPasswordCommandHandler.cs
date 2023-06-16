using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Communication;
using Domain.Interfaces.Communication.Broker;
using Domain.Interfaces.Communication.EmailTemplates;
using Domain.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using WorkingGood.Log;

namespace Application.CQRS.RegisterMessage.Commands.ForgotPasswordMessage;

public class ForgotPasswordCommandHandler : INotificationHandler<ForgotPasswordCommand>
{
    private readonly IWgLog<ForgotPasswordCommandHandler> _logger;
    private readonly IEmailSender _emailSender;
    private readonly IEmailLogSender _emailLogSender;
    private readonly IEmailTemplateDownloader _emailTemplateDownloader;
    public ForgotPasswordCommandHandler(
        IWgLog<ForgotPasswordCommandHandler> logger,
        IEmailSender emailSender,
        IEmailLogSender emailLogSender,
        IEmailTemplateDownloader emailTemplateDownloader)
    {
        _logger = logger;
        _emailSender = emailSender;
        _emailLogSender = emailLogSender;
        _emailTemplateDownloader = emailTemplateDownloader;
    }
    public async Task Handle(ForgotPasswordCommand notification, CancellationToken cancellationToken)
    {
        _logger.Info($"Handling {nameof(ForgotPasswordCommandHandler)}");
        EmailTemplate htmlTemplate =
            await _emailTemplateDownloader.GetByDestination(MessageDestinations.ForgotPasswordEmail);
        string messageContent = GetContent(
            htmlTemplate.Content, 
            notification.ForgotPasswordUrl!, 
            notification.FirstName!,
            notification.LastName!);
        await _emailLogSender.Send(new EmailLog()
        {
            Content = messageContent,
            Type = MessageDestinations.ForgotPasswordEmail.ToString(),
            EmailAddress = notification.Email!
        });
        await _emailSender.Send(
            messageContent,
            "Reset password",
            notification.Email!);
    }
    
    private string GetContent(string htmlContent, string registrationLink, string firstName, string lastName)
    {
        return htmlContent
            .Replace("[Link]", registrationLink)
            .Replace("[first_name]", firstName)
            .Replace("[last_name]", lastName);
    }
}