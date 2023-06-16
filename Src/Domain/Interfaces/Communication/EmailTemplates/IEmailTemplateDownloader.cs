using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces.Communication.EmailTemplates;

public interface IEmailTemplateDownloader
{
    Task<EmailTemplate> GetByDestination(MessageDestinations messageDestinations);
}