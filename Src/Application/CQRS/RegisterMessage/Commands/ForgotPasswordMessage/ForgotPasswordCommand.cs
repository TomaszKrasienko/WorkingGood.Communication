using MediatR;

namespace Application.CQRS.RegisterMessage.Commands.ForgotPasswordMessage;

public class ForgotPasswordCommand : INotification
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ResetToken { get; set; }
}