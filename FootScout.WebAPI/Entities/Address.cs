using System.ComponentModel.DataAnnotations;

namespace FootScout.WebAPI.Entities
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string City { get; set; }
        [MaxLength(30)]
        public string Street { get; set; }
    }
}
