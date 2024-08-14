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
        public DbSet<FavoriteClubAdvertisement> FavoriteClubAdvertisements { get; set; }
        public DbSet<FavoritePlayerAdvertisement> FavoritePlayerAdvertisements { get; set; }
        public DbSet<OfferStatus> OfferStatuses { get; set; }
        public DbSet<PlayerAdvertisement> PlayerAdvertisements { get; set; }
        public DbSet<PlayerFoot> PlayerFeet { get; set; }
        public DbSet<PlayerOffer> PlayerOffers { get; set; }
        public DbSet<PlayerPosition> PlayerPositions { get; set; }
        public DbSet<SalaryRange> SalaryRanges { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClubAdvertisement>()
                .HasOne(ca => ca.User)
                .WithMany()
                .HasForeignKey(ca => ca.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClubAdvertisement>()
               .HasOne(ca => ca.PlayerPosition)
               .WithMany()
               .HasForeignKey(ca => ca.PlayerPositionId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClubAdvertisement>()
                .HasOne(ca => ca.SalaryRange)
                .WithMany()
                .HasForeignKey(ca => ca.SalaryRangeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavoriteClubAdvertisement>()
                .HasOne(caf => caf.ClubAdvertisement)
                .WithMany()
                .HasForeignKey(caf => caf.ClubAdvertisementId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FavoriteClubAdvertisement>()
                .HasOne(caf => caf.User)
                .WithMany()
                .HasForeignKey(caf => caf.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClubOffer>()
                .HasOne(co => co.PlayerAdvertisement)
                .WithMany()
                .HasForeignKey(co => co.PlayerAdvertisementId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClubOffer>()
                .HasOne(co => co.OfferStatus)
                .WithMany()
                .HasForeignKey(co => co.OfferStatusId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClubOffer>()
                .HasOne(co => co.PlayerPosition)
                .WithMany()
                .HasForeignKey(co => co.PlayerPositionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClubOffer>()
                .HasOne(co => co.ClubMember)
                .WithMany()
                .HasForeignKey(co => co.ClubMemberId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClubHistory>()
                .HasOne(ch => ch.Achievements)
                .WithOne()
                .HasForeignKey<ClubHistory>(ch => ch.AchievementsId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClubHistory>()
               .HasOne(ca => ca.PlayerPosition)
               .WithMany()
               .HasForeignKey(ca => ca.PlayerPositionId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClubHistory>()
                .HasOne(ch => ch.User)
                .WithMany()
                .HasForeignKey(ch => ch.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavoritePlayerAdvertisement>()
               .HasOne(paf => paf.PlayerAdvertisement)
               .WithMany()
               .HasForeignKey(paf => paf.PlayerAdvertisementId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FavoritePlayerAdvertisement>()
                .HasOne(paf => paf.User)
                .WithMany()
                .HasForeignKey(paf => paf.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlayerAdvertisement>()
                .HasOne(pa => pa.User)
                .WithMany()
                .HasForeignKey(pa => pa.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlayerAdvertisement>()
               .HasOne(pa => pa.PlayerPosition)
               .WithMany()
               .HasForeignKey(pa => pa.PlayerPositionId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlayerAdvertisement>()
               .HasOne(pa => pa.PlayerFoot)
               .WithMany()
               .HasForeignKey(pa => pa.PlayerFootId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlayerAdvertisement>()
                .HasOne(pa => pa.SalaryRange)
                .WithMany()
                .HasForeignKey(pa => pa.SalaryRangeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlayerOffer>()
                .HasOne(po => po.ClubAdvertisement)
                .WithMany()
                .HasForeignKey(po => po.ClubAdvertisementId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlayerOffer>()
                .HasOne(po => po.OfferStatus)
                .WithMany()
                .HasForeignKey(po => po.OfferStatusId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlayerOffer>()
                .HasOne(po => po.PlayerPosition)
                .WithMany()
                .HasForeignKey(po => po.PlayerPositionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlayerOffer>()
                .HasOne(po => po.PlayerFoot)
                .WithMany()
                .HasForeignKey(po => po.PlayerFootId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlayerOffer>()
                .HasOne(po => po.Player)
                .WithMany()
                .HasForeignKey(po => po.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}