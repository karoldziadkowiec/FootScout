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
        public string Position { get; set; }
        [Required]
        public string League { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string ContractType { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [Required]
        public int SalaryRangeId { get; set; }
        [ForeignKey("SalaryRangeId")]
        public virtual SalaryRange SalaryRange { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
    }
}