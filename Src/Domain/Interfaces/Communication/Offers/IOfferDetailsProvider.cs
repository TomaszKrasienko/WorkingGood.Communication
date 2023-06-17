using Domain.Entities;

namespace Domain.Interfaces.Communication.Offers;

public interface IOfferDetailsProvider
{
    Task<Offer> Provide(Guid offerId);
}