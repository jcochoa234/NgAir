using System.ComponentModel.DataAnnotations;

namespace NgAir.Shared.Entities
{
    public class XeroContact
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Contact ID")]
        [MaxLength(50, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        public required string ContactID { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Name")]
        [MaxLength(250, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        public required string Name { get; set; }

        private string? _Email;
        [MaxLength(150, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string? Email { get { return _Email; } set { _Email = string.IsNullOrWhiteSpace(value) ? null : value; } }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DataType(DataType.DateTime)]
        [Display(Name = "Register Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RegisterDate { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [Display(Name = "Register User")]
        [MaxLength(50, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        public required string RegisterUser { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [Display(Name = "Company")]
        [MaxLength(50, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        public required string Company { get; set; }
    }
}
