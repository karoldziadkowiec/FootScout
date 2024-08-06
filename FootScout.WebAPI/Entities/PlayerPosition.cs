using System.ComponentModel.DataAnnotations;

namespace FootScout.WebAPI.Entities
{
    public class PlayerPosition
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PositionName { get; set; }
    }
}