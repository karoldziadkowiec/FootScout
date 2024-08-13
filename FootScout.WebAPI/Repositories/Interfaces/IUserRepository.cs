using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Models.DTOs;

namespace FootScout.WebAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDTO> GetUser(string userId);
        Task<IEnumerable<UserDTO>> GetUsers();
        Task UpdateUser(UserUpdateDTO userUpdateDTO);
        Task DeleteUser(string userId);
        Task<IEnumerable<ClubHistory>> GetUserClubHistory(string userId);
        Task<IEnumerable<PlayerAdvertisement>> GetUserPlayerAdvertisements(string userId);
        Task<IEnumerable<PlayerAdvertisement>> GetUserActivePlayerAdvertisements(string userId);
        Task<IEnumerable<PlayerAdvertisement>> GetUserInactivePlayerAdvertisements(string userId);
        Task<IEnumerable<PlayerAdvertisementFavorite>> GetUserFavoritePlayerAdvertisements(string userId);
        Task<IEnumerable<PlayerAdvertisementFavorite>> GetUserActiveFavoritePlayerAdvertisements(string userId);
        Task<IEnumerable<PlayerAdvertisementFavorite>> GetUserInactiveFavoritePlayerAdvertisements(string userId);
        Task<IEnumerable<ClubOffer>> GetReceivedClubOffers(string userId);
        Task<IEnumerable<ClubOffer>> GetSentClubOffers(string userId);
        Task<IEnumerable<ClubAdvertisement>> GetUserClubAdvertisements(string userId);
        Task<IEnumerable<ClubAdvertisement>> GetUserActiveClubAdvertisements(string userId);
        Task<IEnumerable<ClubAdvertisement>> GetUserInactiveClubAdvertisements(string userId);
        Task<IEnumerable<ClubAdvertisementFavorite>> GetUserFavoriteClubAdvertisements(string userId);
        Task<IEnumerable<ClubAdvertisementFavorite>> GetUserActiveFavoriteClubAdvertisements(string userId);
        Task<IEnumerable<ClubAdvertisementFavorite>> GetUserInactiveFavoriteClubAdvertisements(string userId);
        Task<IEnumerable<PlayerOffer>> GetReceivedPlayerOffers(string userId);
        Task<IEnumerable<PlayerOffer>> GetSentPlayerOffers(string userId);
    }
}