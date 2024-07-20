using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FootScout.WebAPI.Entities
{
    public class User : IdentityUser
    {
        [MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string LastName { get; set; }
        [MaxLength(9)]
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
    }
}