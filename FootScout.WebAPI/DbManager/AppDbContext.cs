using FootScout.WebAPI.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FootScout.WebAPI.DbManager
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Achievements> Achievements { get; set; }
        public DbSet<ClubAdvertisement> ClubAdvertisements { get; set; }
        public DbSet<ClubHistory> ClubHistories { get; set; }
        public DbSet<ClubOffer> ClubOffers { get; set; }
        public DbSet<PlayerAdvertisement> PlayerAdvertisements { get; set; }
        public DbSet<PlayerOffer> PlayerOffers { get; set; }
        public DbSet<SalaryRange> SalaryRanges { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlayerOffer>()
                .HasOne(po => po.ClubAdvertisement)
                .WithMany()
                .HasForeignKey(po => po.ClubAdvertisementId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PlayerOffer>()
                .HasOne(po => po.UserPlayer)
                .WithMany(u => u.PlayerOffers)
                .HasForeignKey(po => po.UserPlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClubOffer>()
                .HasOne(co => co.PlayerAdvertisement)
                .WithMany()
                .HasForeignKey(co => co.PlayerAdvertisementId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ClubOffer>()
                .HasOne(co => co.UserClub)
                .WithMany(u => u.ClubOffers)
                .HasForeignKey(co => co.UserClubId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClubAdvertisement>()
                .HasOne(ca => ca.User)
                .WithMany(u => u.ClubAdvertisements)
                .HasForeignKey(ca => ca.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClubAdvertisement>()
                .HasOne(ca => ca.SalaryRange)
                .WithMany()
                .HasForeignKey(ca => ca.SalaryRangeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlayerAdvertisement>()
                .HasOne(pa => pa.User)
                .WithMany(u => u.PlayerAdvertisements)
                .HasForeignKey(pa => pa.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlayerAdvertisement>()
                .HasOne(pa => pa.SalaryRange)
                .WithMany()
                .HasForeignKey(pa => pa.SalaryRangeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClubHistory>()
                .HasOne(ch => ch.Achievements)
                .WithOne()
                .HasForeignKey<ClubHistory>(ch => ch.AchievementsId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClubHistory>()
                .HasOne(ch => ch.User)
                .WithMany(u => u.ClubHistories)
                .HasForeignKey(ch => ch.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}