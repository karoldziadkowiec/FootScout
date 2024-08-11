using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FootScout.WebAPI.Entities
{
    public class ClubOffer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PlayerAdvertisementId { get; set; }
        [ForeignKey("PlayerAdvertisementId")]
        public virtual PlayerAdvertisement PlayerAdvertisement { get; set; }
        [Required]
        public int OfferStatusId { get; set; }
        [ForeignKey("OfferStatusId")]
        public virtual OfferStatus OfferStatus { get; set; }
        [Required]
        public int PlayerPositionId { get; set; }
        [ForeignKey("PlayerPositionId")]
        public virtual PlayerPosition PlayerPosition { get; set; }
        [Required]
        public string ClubName { get; set; }
        [Required]
        public string League { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public double Salary { get; set; }
        public string AdditionalInformation { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public string UserClubId { get; set; }
        [ForeignKey("UserClubId")]
        public virtual User UserClub { get; set; }
    }
}