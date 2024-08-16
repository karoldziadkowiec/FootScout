﻿using FootScout.WebAPI.Entities;

namespace FootScout.WebAPI.Repositories.Interfaces
{
    public interface IClubAdvertisementRepository
    {
        Task<ClubAdvertisement> GetClubAdvertisement(int clubAdvertisementId);
        Task<IEnumerable<ClubAdvertisement>> GetAllClubAdvertisements();
        Task<IEnumerable<ClubAdvertisement>> GetActiveClubAdvertisements();
        Task<IEnumerable<ClubAdvertisement>> GetInactiveClubAdvertisements();
        Task CreateClubAdvertisement(ClubAdvertisement clubAdvertisement);
        Task UpdateClubAdvertisement(ClubAdvertisement clubAdvertisement);
        Task DeleteClubAdvertisement(int clubAdvertisementId);
    }
}