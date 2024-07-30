using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FootScout.WebAPI.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public virtual ICollection<ClubHistory> ClubHistories { get; set; } = new List<ClubHistory>();
        public virtual ICollection<PlayerAdvertisement> PlayerAdvertisements { get; set; } = new List<PlayerAdvertisement>();
        public virtual ICollection<PlayerOffer> PlayerOffers { get; set; } = new List<PlayerOffer>();
        public virtual ICollection<ClubAdvertisement> ClubAdvertisements { get; set; } = new List<ClubAdvertisement>();
        public virtual ICollection<ClubOffer> ClubOffers { get; set; } = new List<ClubOffer>();
    }
}