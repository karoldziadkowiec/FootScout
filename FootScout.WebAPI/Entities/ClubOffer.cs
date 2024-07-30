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
        public string UserClubId { get; set; }
        [ForeignKey("UserClubId")]
        public virtual User UserClub { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
    }
}