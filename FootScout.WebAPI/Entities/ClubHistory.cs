using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FootScout.WebAPI.Entities
{
    public class ClubHistory
    {
        [Key]
        public int Id { get; set; }
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
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int AchievementsId { get; set; }
        [ForeignKey("AchievementsId")]
        public virtual Achievements Achievements { get; set; }
        [Required]
        public string PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public virtual User Player { get; set; }
    }
}