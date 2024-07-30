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
        public string Status { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public string UserPlayerId { get; set; }
        [ForeignKey("UserPlayerId")]
        public virtual User UserPlayer { get; set; }
    }
}