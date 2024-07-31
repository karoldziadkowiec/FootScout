using FootScout.WebAPI.Entities;

namespace FootScout.WebAPI.Repositories.Interfaces
{
    public interface IAchievementsRepository
    {
        Task CreateAchievements(Achievements achievements);
    }
}