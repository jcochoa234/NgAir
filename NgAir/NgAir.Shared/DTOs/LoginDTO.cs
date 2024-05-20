using System.ComponentModel.DataAnnotations;

namespace NgAir.Shared.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "The field {0} is required.")]
        [EmailAddress(ErrorMessage = "You must enter a valid email address.")]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "The field {0} is required.")]
        [MinLength(6, ErrorMessage = "The {0} field must have at least {1} characters.")]
        public string Password { get; set; } = null!;
    }
}
