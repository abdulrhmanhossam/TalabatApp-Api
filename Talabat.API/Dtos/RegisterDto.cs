using System.ComponentModel.DataAnnotations;

namespace Talabat.API.Dtos
{
    public class RegisterDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Phone]
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).*$",
            ErrorMessage = "Password must contain at least 1 uppercase letter," +
            " 1 lowercase letter, 1 digit, and 1 special character.")]
        public string Password { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }
}
