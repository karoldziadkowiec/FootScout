using System.ComponentModel.DataAnnotations;

namespace FootScout.WebAPI.Models.DTOs
{
    public class UserUpdateDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string ConfirmPasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
    }
}
