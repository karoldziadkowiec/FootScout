using FootScout.WebAPI.Entities;

namespace FootScout.WebAPI.Repositories.Interfaces
{
    public interface IOfferStatusRepository
    {
        Task<IEnumerable<OfferStatus>> GetOfferStatuses();
        Task<OfferStatus> GetOfferStatus(int offerStatusId);
        Task<string> GetOfferStatusName(int offerStatusId);
        Task<int> GetOfferStatusId(string offerStatusName);
    }
}