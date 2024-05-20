using System.ComponentModel.DataAnnotations;

namespace NgAir.Shared.DTOs
{
    public class ResetPasswordDTO
    {
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "You must enter a valid email address.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Field {0} must have between {2} and {1} characters.")]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "New password and confirmation are not the same.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Field {0} must have between {2} and {1} characters.")]
        public string ConfirmPassword { get; set; } = null!;

        public string Token { get; set; } = null!;
    }
}
