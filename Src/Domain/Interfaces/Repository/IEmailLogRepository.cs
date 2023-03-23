using Domain.Entities;

namespace Domain.Interfaces.Repository;

public interface IEmailLogRepository
{
    Task AddLog(EmailLog emailLog);
}