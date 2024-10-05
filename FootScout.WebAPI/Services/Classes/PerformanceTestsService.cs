using FootScout.WebAPI.DbManager;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Models.Constants;
using FootScout.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FootScout.WebAPI.Services.Classes
{
    public class PerformanceTestsService : IPerformanceTestsService
    {
        private readonly AppDbContext _dbContext;

        public PerformanceTestsService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SeedComponents(int testCounter)
        {
            await SeedUsers(testCounter);
            await SeedClubHistories(testCounter);
            await SeedProblems(testCounter);
            await SeedChats(testCounter);
            await SeedMessages(testCounter);
            await SeedPlayerAdvertisements(testCounter);
            await SeedClubOffers(testCounter);
            await SeedClubAdvertisements(testCounter);
            await SeedPlayerOffers(testCounter);
        }

        public async Task ClearDatabaseOfSeededComponents()
        {
            await ClearAchievements();
            await ClearClubHistories();
            await ClearProblems();
            await ClearMessages();
            await ClearChats();
            await ClearSalaryRanges();
            await ClearPlayerAdvertisements();
            await ClearClubOffers();
            await ClearClubAdvertisements();
            await ClearPlayerOffers();
            await ClearUsers();
        }

        // Seeding
        private async Task SeedUsers(int testCounter)
        {
            var users = new List<User>();
            var userRoles = new List<IdentityUserRole<string>>();
            var passwordHasher = new PasswordHasher<User>();

            var userRoleId = _dbContext.Roles.First(r => r.Name == Role.User).Id;

            // users
            for (int i = 1; i <= testCounter; i++)
            {
                string userId = $"user{i}";

                var user = new User
                {
                    Id = userId,
                    Email = $"user{i}@mail.com",
                    NormalizedEmail = $"user{i}@mail.com".ToUpper(),
                    UserName = $"user{i}@mail.com",
                    NormalizedUserName = $"user{i}@mail.com".ToUpper(),
                    FirstName = $"FirstName {i}",
                    LastName = $"LastName {i}",
                    Location = $"Location {i}",
                    PhoneNumber = $"123456789",
                    CreationDate = DateTime.Now
                };
                user.PasswordHash = passwordHasher.HashPassword(user, $"Password{i}!");
                users.Add(user);

                // user roles
                userRoles.Add(new IdentityUserRole<string>
                {
                    UserId = userId,
                    RoleId = userRoleId
                });
            }
            await _dbContext.Users.AddRangeAsync(users);
            await _dbContext.SaveChangesAsync();

            await _dbContext.UserRoles.AddRangeAsync(userRoles);
            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedClubHistories(int testCounter)
        {
            var achievements = new List<Achievements>();
            var clubHistories = new List<ClubHistory>();

            // achievements
            for (int i = 1; i <= testCounter; i++)
            {
                achievements.Add(new Achievements
                {
                    NumberOfMatches = i,
                    Goals = i,
                    Assists = i,
                    AdditionalAchievements = $"Achievement {i}"
                });
            }
            await _dbContext.Achievements.AddRangeAsync(achievements);
            await _dbContext.SaveChangesAsync();

            // club histories
            var achievementIds = await _dbContext.Achievements.Select(a => a.Id).Take(testCounter).ToListAsync();
            for (int i = 1; i <= testCounter; i++)
            {
                clubHistories.Add(new ClubHistory
                {
                    PlayerPositionId = 1,
                    ClubName = $"ClubName {i}",
                    League = $"League {i}",
                    Region = $"Region {i}",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(150),
                    AchievementsId = achievementIds[i - 1],
                    PlayerId = $"user{i}"
                });
            }
            await _dbContext.ClubHistories.AddRangeAsync(clubHistories);
            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedProblems(int testCounter)
        {
            var problems = new List<Problem>();

            // problems
            for (int i = 1; i <= testCounter; i++)
            {
                var isSolvedParameter = (i % 2 == 0) ? true : false;

                problems.Add(new Problem
                {
                    Title = $"Title {i}",
                    Description = $"Description {i}",
                    CreationDate = DateTime.Now,
                    IsSolved = isSolvedParameter,
                    RequesterId = $"user{i}"
                });
            }
            await _dbContext.Problems.AddRangeAsync(problems);
            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedChats(int testCounter)
        {
            var chats = new List<Chat>();

            // chats
            for (int i = 1; i <= testCounter; i++)
            {
                var user1Id = (i == 1) ? "user2" : "user1";
                var user2Id = (i == 1) ? "user3" : $"user{i}";

                chats.Add(new Chat
                {
                    User1Id = user1Id,
                    User2Id = user2Id
                });
            }
            await _dbContext.Chats.AddRangeAsync(chats);
            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedMessages(int testCounter)
        {
            var messages = new List<Message>();

            // messages
            var chatIds = await _dbContext.Chats.Select(c => c.Id).Take(testCounter).ToListAsync();
            for (int i = 1; i <= testCounter; i++)
            {
                var senderId = (i == 1) ? "user2" : "user1";
                var receiverId = (i == 1) ? "user3" : $"user{i}";

                messages.Add(new Message
                {
                    ChatId = chatIds[i - 1],
                    Content = $"Message {i}",
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    Timestamp = DateTime.Now
                });
            }
            await _dbContext.Messages.AddRangeAsync(messages);
            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedPlayerAdvertisements(int testCounter)
        {
            var salaryRanges = new List<SalaryRange>();
            var playerAdvertisements = new List<PlayerAdvertisement>();
            var favoritePlayerAdvertisements = new List<FavoritePlayerAdvertisement>();

            // salary ranges
            for (int i = 1; i <= testCounter; i++)
            {
                salaryRanges.Add(new SalaryRange
                {
                    Min = i,
                    Max = i + 1
                });
            }
            await _dbContext.SalaryRanges.AddRangeAsync(salaryRanges);
            await _dbContext.SaveChangesAsync();

            // player advertisements
            var salaryRangeIds = await _dbContext.SalaryRanges.Select(sr => sr.Id).Take(testCounter).ToListAsync();
            for (int i = 1; i <= testCounter; i++)
            {
                playerAdvertisements.Add(new PlayerAdvertisement
                {
                    PlayerPositionId = 1,
                    League = $"League {i}",
                    Region = $"Region {i}",
                    Age = i,
                    Height = i,
                    PlayerFootId = 1,
                    SalaryRangeId = salaryRangeIds[i - 1],
                    CreationDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(30),
                    PlayerId = $"user{i}"
                });
            }
            await _dbContext.PlayerAdvertisements.AddRangeAsync(playerAdvertisements);
            await _dbContext.SaveChangesAsync();

            // favorite player advertisements
            var playerAdvertisementIds = await _dbContext.PlayerAdvertisements.Select(pa => pa.Id).Take(testCounter).ToListAsync();
            for (int i = 1; i <= testCounter; i++)
            {
                var userId = (i == testCounter) ? $"user1" : $"user{i + 1}";

                favoritePlayerAdvertisements.Add(new FavoritePlayerAdvertisement
                {
                    PlayerAdvertisementId = playerAdvertisementIds[i - 1],
                    UserId = userId
                });
            }
            await _dbContext.FavoritePlayerAdvertisements.AddRangeAsync(favoritePlayerAdvertisements);
            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedClubOffers(int testCounter)
        {
            var clubOffers = new List<ClubOffer>();

            // club offers
            var playerAdvertisementIds = await _dbContext.PlayerAdvertisements.Select(pa => pa.Id).Take(testCounter).ToListAsync();
            for (int i = 1; i <= testCounter; i++)
            {
                var counter = (i == testCounter) ? 1 : i+1;

                clubOffers.Add(new ClubOffer
                {
                    PlayerAdvertisementId = playerAdvertisementIds[i - 1],
                    OfferStatusId = 1,
                    PlayerPositionId = 1,
                    ClubName = $"ClubName {counter}",
                    League = $"League {counter}",
                    Region = $"Region {counter}",
                    Salary = counter,
                    AdditionalInformation = $"Info {counter}",
                    CreationDate = DateTime.Now,
                    ClubMemberId = $"user{counter}"
                });
            }
            await _dbContext.ClubOffers.AddRangeAsync(clubOffers);
            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedClubAdvertisements(int testCounter)
        {
            var salaryRanges = new List<SalaryRange>();
            var clubAdvertisements = new List<ClubAdvertisement>();
            var favoriteClubAdvertisements = new List<FavoriteClubAdvertisement>();

            // salary ranges
            for (int i = 1; i <= testCounter; i++)
            {
                salaryRanges.Add(new SalaryRange
                {
                    Min = i,
                    Max = i + 1
                });
            }
            await _dbContext.SalaryRanges.AddRangeAsync(salaryRanges);
            await _dbContext.SaveChangesAsync();

            // club advertisements
            var salaryRangeIds = await _dbContext.SalaryRanges.Select(sr => sr.Id).ToListAsync();
            for (int i = 1; i <= testCounter; i++)
            {
                clubAdvertisements.Add(new ClubAdvertisement
                {
                    PlayerPositionId = 1,
                    ClubName = $"ClubName {i}",
                    League = $"League {i}",
                    Region = $"Region {i}",
                    SalaryRangeId = salaryRangeIds[testCounter + i - 1],
                    CreationDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(30),
                    ClubMemberId = $"user{i}"
                });
            }
            await _dbContext.ClubAdvertisements.AddRangeAsync(clubAdvertisements);
            await _dbContext.SaveChangesAsync();

            // favorite club advertisements
            var clubAdvertisementIds = await _dbContext.ClubAdvertisements.Select(ca => ca.Id).Take(testCounter).ToListAsync();
            for (int i = 1; i <= testCounter; i++)
            {
                var userId = (i == testCounter) ? $"user1" : $"user{i + 1}";

                favoriteClubAdvertisements.Add(new FavoriteClubAdvertisement
                {
                    ClubAdvertisementId = clubAdvertisementIds[i - 1],
                    UserId = userId
                });
            }
            await _dbContext.FavoriteClubAdvertisements.AddRangeAsync(favoriteClubAdvertisements);
            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedPlayerOffers(int testCounter)
        {
            var playerOffers = new List<PlayerOffer>();

            // club offers
            var clubAdvertisementIds = await _dbContext.ClubAdvertisements.Select(ca => ca.Id).Take(testCounter).ToListAsync();
            for (int i = 1; i <= testCounter; i++)
            {
                var counter = (i == testCounter) ? 1 : i + 1;

                playerOffers.Add(new PlayerOffer
                {
                    ClubAdvertisementId = clubAdvertisementIds[i - 1],
                    OfferStatusId = 1,
                    PlayerPositionId = 1,
                    Age = counter,
                    Height = counter,
                    PlayerFootId = 1,
                    Salary = counter,
                    AdditionalInformation = $"Info {counter}",
                    CreationDate = DateTime.Now,
                    PlayerId = $"user{counter}"
                });
            }
            await _dbContext.PlayerOffers.AddRangeAsync(playerOffers);
            await _dbContext.SaveChangesAsync();
        }

        // Clearing
        private async Task ClearAchievements()
        {
            var achievements = await _dbContext.Achievements.ToListAsync();

            _dbContext.Achievements.RemoveRange(achievements);
            await _dbContext.SaveChangesAsync();
        }

        private async Task ClearClubHistories()
        {
            var clubHistories = await _dbContext.ClubHistories.ToListAsync();

            _dbContext.ClubHistories.RemoveRange(clubHistories);
            await _dbContext.SaveChangesAsync();
        }

        private async Task ClearProblems()
        {
            var problems = await _dbContext.Problems.ToListAsync();

            _dbContext.Problems.RemoveRange(problems);
            await _dbContext.SaveChangesAsync();
        }

        private async Task ClearMessages()
        {
            var messages = await _dbContext.Messages.ToListAsync();

            _dbContext.Messages.RemoveRange(messages);
            await _dbContext.SaveChangesAsync();
        }

        private async Task ClearChats()
        {
            var chats = await _dbContext.Chats.ToListAsync();

            _dbContext.Chats.RemoveRange(chats);
            await _dbContext.SaveChangesAsync();
        }

        private async Task ClearSalaryRanges()
        {
            await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM SalaryRanges");
            await _dbContext.SaveChangesAsync();
        }

        private async Task ClearPlayerAdvertisements()
        {
            var favoritePlayerAdvertisements = await _dbContext.FavoritePlayerAdvertisements.ToListAsync();
            _dbContext.FavoritePlayerAdvertisements.RemoveRange(favoritePlayerAdvertisements);
            await _dbContext.SaveChangesAsync();

            await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM PlayerAdvertisements");
            await _dbContext.SaveChangesAsync();
        }

        private async Task ClearClubOffers()
        {
            var clubOffers = await _dbContext.ClubOffers.ToListAsync();

            _dbContext.ClubOffers.RemoveRange(clubOffers);
            await _dbContext.SaveChangesAsync();
        }

        private async Task ClearClubAdvertisements()
        {
            var favoriteClubAdvertisements = await _dbContext.FavoriteClubAdvertisements.ToListAsync();
            _dbContext.FavoriteClubAdvertisements.RemoveRange(favoriteClubAdvertisements);
            await _dbContext.SaveChangesAsync();

            await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM ClubAdvertisements");
            await _dbContext.SaveChangesAsync();
        }

        private async Task ClearPlayerOffers()
        {
            var playerOffers = await _dbContext.PlayerOffers.ToListAsync();

            _dbContext.PlayerOffers.RemoveRange(playerOffers);
            await _dbContext.SaveChangesAsync();
        }

        private async Task ClearUsers()
        {
            var adminEmail = "admin@admin.com";
            var unknownEmail = "unknown@unknown.com";

            await _dbContext.Database.ExecuteSqlRawAsync(@"DELETE FROM AspNetUserRoles WHERE UserId NOT IN (SELECT Id FROM AspNetUsers WHERE Email IN ({0}, {1}))", adminEmail, unknownEmail);
            await _dbContext.SaveChangesAsync();

            await _dbContext.Database.ExecuteSqlRawAsync(@"DELETE FROM AspNetUsers WHERE Email NOT IN ({0}, {1})", adminEmail, unknownEmail);
            await _dbContext.SaveChangesAsync();
        }
    }
}