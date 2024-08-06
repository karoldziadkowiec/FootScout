using FootScout.WebAPI.Entities;

namespace FootScout.WebAPI.Repositories.Interfaces
{
    public interface IPlayerFootRepository
    {
        Task<IEnumerable<PlayerFoot>> GetPlayerFeet();
        Task<string> GetPlayerFootName(int footId);
    }
}