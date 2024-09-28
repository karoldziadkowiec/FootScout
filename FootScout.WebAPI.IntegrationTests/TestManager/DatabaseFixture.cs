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

            UserTokenJWT = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjlmMDRmNDZkLTZlMjQtNGZjOC1hNDkwLTA5NmFmODhmMWQzOCIsImp0aSI6IjQwMzkzOWZjLTk1MDEtNGI1MS04YzczLTExZjIxYjhhMDYyYyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVzZXIiLCJleHAiOjE3MjczNTg4ODEsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcyMjAiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjMwMDAifQ.AIAGsJuUywTl-nyrHstovoiGwVoczYfAFWNNRY_n8OQ";
            AdminTokenJWT = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImNjYTg1MTI0LTkwNTctNDAzYy1hNTM2LWJlNjA2MDBkMTNmNCIsImp0aSI6IjgxZThkYjI2LTNiMDktNDNlNC1hZmU2LTg2MWZlNGRlOTI4ZCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzI3MzU4ODQ4LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIwIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDozMDAwIn0.6-fzr6rZVHf5crNCL-lB18mf0a9UCGWPAnsTR4WzQeU";

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
