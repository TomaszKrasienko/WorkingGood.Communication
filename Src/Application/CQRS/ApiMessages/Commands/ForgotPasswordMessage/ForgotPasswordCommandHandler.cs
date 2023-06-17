using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Communication;
using Domain.Interfaces.Communication.EmailTemplates;
using MediatR;
using WorkingGood.Log;

namespace Application.CQRS.RegisterMessage.Commands.ForgotPasswordMessage;

public class ForgotPasswordCommandHandler : INotificationHandler<ForgotPasswordCommand>
{
    private readonly IWgLog<ForgotPasswordCommandHandler> _logger;
    private readonly IEmailSender _emailSender;
    private readonly IEmailTemplateDownloader _emailTemplateDownloader;
    public ForgotPasswordCommandHandler(
        IWgLog<ForgotPasswordCommandHandler> logger,
        IEmailSender emailSender,
        IEmailTemplateDownloader emailTemplateDownloader)
    {
        _logger = logger;
        _emailSender = emailSender;
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
        _logger.Email(MessageDestinations.ForgotPasswordEmail.ToString(), notification.Email!, notification.ForgotPasswordUrl!);
        await _emailSender.Send(
            messageContent,
            "Reset password",
            notification.Email!);
    }
    
    private string GetContent(string htmlContent, string registrationLink, string firstName, string lastName)
    {
        return htmlContent
            .Replace("[link]", registrationLink)
            .Replace("[first_name]", firstName)
            .Replace("[last_name]", lastName);
    }
}