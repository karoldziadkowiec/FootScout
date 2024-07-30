using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootScout.WebAPI.Entities
{
    public class PlayerAdvertisementFavorite
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PlayerAdvertisementId { get; set; }
        [ForeignKey("PlayerAdvertisementId")]
        public PlayerAdvertisement PlayerAdvertisement { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}