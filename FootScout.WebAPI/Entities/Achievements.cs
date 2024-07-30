using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootScout.WebAPI.Entities
{
    public class Achievements
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int NumberOfMatches { get; set; }
        [Required]
        public int Goals { get; set; }
        [Required]
        public int Assists { get; set; }
        public string AdditionalAchievements { get; set; }
    }
}