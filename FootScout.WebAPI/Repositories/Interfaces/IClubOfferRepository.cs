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
        Task AcceptClubOffer(ClubOffer clubOffer);
        Task RejectClubOffer(ClubOffer clubOffer);
        Task<int> GetClubOfferStatusId(int playerAdvertisementId, string userId);
    }
}