using Domain.Interfaces.Communication;
using MediatR;

namespace Application.CQRS.RegisterMessage.Commands.ForgotPasswordMessage;

public class ForgotPasswordCommandHandler : INotificationHandler<ForgotPasswordCommand>
{
    private readonly IEmailSender _emailSender;
    public ForgotPasswordCommandHandler(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }
    public async Task Handle(ForgotPasswordCommand notification, CancellationToken cancellationToken)
    {
        await _emailSender.Send(
            $"Your reset token: {notification.ResetToken}",
            "Reset password",
            notification.Email!);
    }
}