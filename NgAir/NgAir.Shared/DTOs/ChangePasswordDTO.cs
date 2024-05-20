using System.ComponentModel.DataAnnotations;

namespace NgAir.Shared.DTOs
{
    public class ChangePasswordDTO
    {
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Field {0} must have between {2} and {1} characters")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string CurrentPassword { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Field {0} must have between {2} and {1} characters")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string NewPassword { get; set; } = null!;

        [Compare("NewPassword", ErrorMessage = "New password and confirmation are not the same.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Field {0} must have between {2} and {1} characters")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Confirm { get; set; } = null!;
    }
}
