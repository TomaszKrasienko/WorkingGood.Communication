using Domain.Interfaces.Communication;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.RegisterMessage.Commands.ForgotPasswordMessage;

public class ForgotPasswordCommandHandler : INotificationHandler<ForgotPasswordCommand>
{
    private readonly ILogger<ForgotPasswordCommandHandler> _logger;
    private readonly IEmailSender _emailSender;
    public ForgotPasswordCommandHandler(ILogger<ForgotPasswordCommandHandler> logger, IEmailSender emailSender)
    {
        _emailSender = emailSender;
        _logger = logger;
    }
    public async Task Handle(ForgotPasswordCommand notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling ForgotPasswordCommand");
        await _emailSender.Send(
            $"Your reset token: {notification.ResetToken}",
            "Reset password",
            notification.Email!);
    }
}