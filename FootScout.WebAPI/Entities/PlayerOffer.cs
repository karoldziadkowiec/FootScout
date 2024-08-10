using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FootScout.WebAPI.Entities
{
    public class PlayerOffer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ClubAdvertisementId { get; set; }
        [ForeignKey("ClubAdvertisementId")]
        public virtual ClubAdvertisement ClubAdvertisement { get; set; }
        [Required]
        public int AdvertisementStatusId { get; set; }
        [ForeignKey("AdvertisementStatusId")]
        public virtual AdvertisementStatus AdvertisementStatus { get; set; }
        [Required]
        public int PlayerPositionId { get; set; }
        [ForeignKey("PlayerPositionId")]
        public virtual PlayerPosition PlayerPosition { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        public int PlayerFootId { get; set; }
        [ForeignKey("PlayerFootId")]
        public virtual PlayerFoot PlayerFoot { get; set; }
        [Required]
        public double Salary { get; set; }
        public string AdditionalInformation { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string UserPlayerId { get; set; }
        [ForeignKey("UserPlayerId")]
        public virtual User UserPlayer { get; set; }
    }
}