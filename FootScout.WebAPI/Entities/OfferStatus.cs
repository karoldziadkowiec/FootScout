using System.ComponentModel.DataAnnotations;

namespace FootScout.WebAPI.Entities
{
    public class OfferStatus
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string StatusName { get; set; }
    }
}