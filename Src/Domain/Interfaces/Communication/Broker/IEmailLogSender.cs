using Domain.Entities;

namespace Domain.Interfaces.Communication.Broker;

public interface IEmailLogSender
{
    Task Send(EmailLog emailLog);
}