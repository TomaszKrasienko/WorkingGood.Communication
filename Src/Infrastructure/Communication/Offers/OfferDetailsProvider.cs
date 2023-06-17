using System.Net.Http.Json;
using Domain.Entities;
using Domain.Interfaces.Communication.Offers;
using Infrastructure.Common.Statics;
using WorkingGood.Log;

namespace Infrastructure.Communication.Offers;

public class OfferDetailsProvider : IOfferDetailsProvider
{
    private readonly IWgLog<OfferDetailsProvider> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    public OfferDetailsProvider(IWgLog<OfferDetailsProvider> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }
    public async Task<Offer> Provide(Guid offerId)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient(HttpClients.WgApi);
            var response = await httpClient.GetAsync(
                $"/offers/getOfferById/{offerId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Offer>() ?? new Offer();
            }
            else
            {
                throw new Exception("There is problem with getting offerDetails");
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.Error(ex);
            throw;
        }
    }
}