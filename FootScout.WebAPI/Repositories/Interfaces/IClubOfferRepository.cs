using FootScout.WebAPI.Entities;

namespace FootScout.WebAPI.Repositories.Interfaces
{
    public interface IClubOfferRepository
    {
        Task<ClubOffer> GetClubOffer(int clubOfferId);
        Task<IEnumerable<ClubOffer>> GetClubOffers();
        Task<IEnumerable<ClubOffer>> GetActiveClubOffers();
        Task<IEnumerable<ClubOffer>> GetInactiveClubOffers();
        Task CreateClubOffer(ClubOffer clubOffer);
        Task UpdateClubOffer(ClubOffer clubOffer);
        Task<int> CheckClubOfferIsSubmitted(int playerAdvertisementId, string userId);
    }
}