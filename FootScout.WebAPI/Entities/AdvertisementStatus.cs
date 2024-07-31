using System.ComponentModel.DataAnnotations;

namespace FootScout.WebAPI.Entities
{
    public class AdvertisementStatus
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string StatusName { get; set; }
    }
}