using MediatR;

namespace Application.CQRS.RegisterMessage.Commands.ForgotPasswordMessage;

public record ForgotPasswordCommand : INotification
{
    public string? Email { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? ForgotPasswordUrl { get; init; }
}