using System.ComponentModel.DataAnnotations;

namespace FootScout.WebAPI.Entities
{
    public class PlayerFoot
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string FootName { get; set; }
    }
}