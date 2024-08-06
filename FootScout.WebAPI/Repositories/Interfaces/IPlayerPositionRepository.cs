using FootScout.WebAPI.Entities;

namespace FootScout.WebAPI.Repositories.Interfaces
{
    public interface IPlayerPositionRepository
    {
        Task<IEnumerable<PlayerPosition>> GetPlayerPositions();
        Task<string> GetPlayerPositionName(int positionId);
    }
}