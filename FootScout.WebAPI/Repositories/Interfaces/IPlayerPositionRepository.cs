using FootScout.WebAPI.Entities;

namespace FootScout.WebAPI.Repositories.Interfaces
{
    public interface IPlayerPositionRepository
    {
        Task<IEnumerable<PlayerPosition>> GetPlayerPositions();
        Task<int> GetPlayerPositionCount();
        Task<string> GetPlayerPositionName(int positionId);
        Task<bool> CheckPlayerPositionExists(string positionName);
        Task CreatePlayerPosition(PlayerPosition playerPosition);
    }
}