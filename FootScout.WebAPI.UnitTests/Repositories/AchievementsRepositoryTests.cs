﻿using FootScout.WebAPI.DbManager;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Repositories.Classes;
using FootScout.WebAPI.UnitTests.TestManager;

namespace FootScout.WebAPI.UnitTests.Repositories
{
    public class AchievementsRepositoryTests : TestBase
    {
        [Fact]
        public async Task CreateAchievements_AddsNewAchievements()
        {
            // Arrange
            var options = GetDbContextOptions("CreateAchievements_AddsNewAchievements");
            var newAchievements = new Achievements
            {
                Id = 3,
                NumberOfMatches = 80,
                Goals = 60,
                Assists = 45,
                AdditionalAchievements = "no"
            };

            using (var dbContext = new AppDbContext(options))
            {
                await SeedClubHistoryTestBase(dbContext);
                var _achievementsRepository = new AchievementsRepository(dbContext);

                // Act
                await _achievementsRepository.CreateAchievements(newAchievements);

                // Assert
                var result = await dbContext.Achievements.FindAsync(3);
                Assert.NotNull(result);
                Assert.Equal(80, result.NumberOfMatches);
                Assert.Equal(60, result.Goals);
                Assert.Equal(45, result.Assists);
            }
        }
    }
}