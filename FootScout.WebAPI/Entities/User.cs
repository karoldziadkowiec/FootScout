using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FootScout.WebAPI.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
    }
}