using System.ComponentModel.DataAnnotations;

namespace FootScout.WebAPI.Entities
{
    public class PlayerFoot
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FootName { get; set; }
    }
}