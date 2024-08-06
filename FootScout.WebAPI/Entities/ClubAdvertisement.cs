using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FootScout.WebAPI.Entities
{
    public class ClubAdvertisement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ClubName { get; set; }
        [Required]
        public int PlayerPositionId { get; set; }
        [ForeignKey("PlayerPositionId")]
        public virtual PlayerPosition PlayerPosition { get; set; }
        [Required]
        public string League { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        public int PlayerFootId { get; set; }
        [ForeignKey("PlayerFootId")]
        public virtual PlayerFoot PlayerFoot { get; set; }
        [Required]
        public int SalaryRangeId { get; set; }
        [ForeignKey("SalaryRangeId")]
        public virtual SalaryRange SalaryRange { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}