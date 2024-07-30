using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FootScout.WebAPI.Entities
{
    public class ClubAdvertisementFavorite
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ClubAdvertisementId { get; set; }
        [ForeignKey("ClubAdvertisementId")]
        public ClubAdvertisement ClubAdvertisement { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}