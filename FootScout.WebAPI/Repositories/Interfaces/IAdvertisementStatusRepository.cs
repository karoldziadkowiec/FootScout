using FootScout.WebAPI.Entities;

namespace FootScout.WebAPI.Repositories.Interfaces
{
    public interface IAdvertisementStatusRepository
    {
        Task<IEnumerable<AdvertisementStatus>> GetAdvertisementStatuses();
        Task<string> GetAdvertisementStatusName(int advertisementStatusId);
        Task<int> GetAdvertisementStatusId(string advertisementStatusName);
    }
}