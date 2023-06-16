using System.Net.Http.Json;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Communication.EmailTemplates;
using Infrastructure.Common.Statics;
using WorkingGood.Log;

namespace Infrastructure.Communication.EmailTemplates;

public class EmailTemplateDownloader : IEmailTemplateDownloader
{
    private readonly IWgLog<EmailTemplateDownloader> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    public EmailTemplateDownloader(IWgLog<EmailTemplateDownloader> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }
    public async Task<EmailTemplate> GetByDestination(MessageDestinations messageDestinations)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient(HttpClients.WgTool);
            var response = await httpClient.GetAsync(
                    $"api/emailTemplates/getEmailTemplateByDestination/{messageDestinations.ToString()}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EmailTemplate>() ?? new EmailTemplate();
            }
            else
            {
                throw new Exception("There is problem with getting email template");
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.Error(ex);
            throw;
        }
    }
}