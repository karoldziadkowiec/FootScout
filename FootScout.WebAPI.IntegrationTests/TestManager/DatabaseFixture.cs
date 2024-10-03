using FootScout.WebAPI.DbManager;

namespace FootScout.WebAPI.IntegrationTests.TestManager
{
    public class DatabaseFixture : IDisposable
    {
        public AppDbContext DbContext { get; private set; }
        public string UserTokenJWT { get; private set; }
        public string AdminTokenJWT { get; private set; }

        public DatabaseFixture()
        {
            var dbName = $"FootScoutTests_{Guid.NewGuid()}";
            var options = TestBase.GetDbContextOptions($"Server=.; Database={dbName}; Integrated Security=true; TrustServerCertificate=True; MultipleActiveResultSets=true");
            DbContext = new AppDbContext(options);

            UserTokenJWT = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImEwZjk1ZDFiLTc0MjAtNDYzNi1iOTBlLTJiYjUwYTAxMzJiNSIsImp0aSI6IjAwMzBiZmRjLWVkYTgtNDQ2NC1hOGEyLWNmNjBiZWMxODI5ZSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVzZXIiLCJleHAiOjE3MjgwNDExNjUsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcyMjAiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjMwMDAifQ.-OhnsnjXWf-CSvccZn_yzkBTeX_UBOB2po1kZrO7hoQ";
            AdminTokenJWT = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImNhNDY5MTExLTFmZDAtNDBkYi1iNTAzLWE3ZDU2YWUwMzA1NSIsImp0aSI6IjY4ZmZlNGE4LWM1NzYtNGZkZC05OTI4LTNjZDlmNjJiNGUxZCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzI4MDQxMTM4LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIwIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDozMDAwIn0.TUVjnICxOuWuG_N-qLkq35lz0bgn48h2zqasD12ROkQ";

            InitializeDatabase().GetAwaiter().GetResult();
        }

        private async Task InitializeDatabase()
        {
            await DbContext.Database.EnsureCreatedAsync();

            await TestBase.SeedRoleTestBase(DbContext);
            await TestBase.SeedPlayerPositionTestBase(DbContext);
            await TestBase.SeedPlayerFootTestBase(DbContext);
            await TestBase.SeedOfferStatusTestBase(DbContext);
            await TestBase.SeedUserTestBase(DbContext, TestBase.CreateUserManager(DbContext));
            await TestBase.SeedClubHistoryTestBase(DbContext);
            await TestBase.SeedProblemTestBase(DbContext);
            await TestBase.SeedChatTestBase(DbContext);
            await TestBase.SeedMessageTestBase(DbContext);
            await TestBase.SeedPlayerAdvertisementTestBase(DbContext);
            await TestBase.SeedClubOfferTestBase(DbContext);
            await TestBase.SeedClubAdvertisementTestBase(DbContext);
            await TestBase.SeedPlayerOfferTestBase(DbContext);
        }

        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
            DbContext.Dispose();
        }
    }
}
