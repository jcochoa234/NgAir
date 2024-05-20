using NgAir.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace NgAir.Shared.DTOs
{
    public class UserDTO : User
    {
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Field {0} must have between {2} and {1} characters.")]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "New password and confirmation are not the same.")]
        [Display(Name = "Password Confirm")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Field {0} must have between {2} and {1} characters.")]
        public string PasswordConfirm { get; set; } = null!;
    }
}
