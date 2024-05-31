using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NgAir.Shared.Entities
{
    [Table("XeroContainerHeaders")]
    public class XeroContainerHeader
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Container ID")]
        [MaxLength(50, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        public string? ContainerNumber { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DataType(DataType.DateTime)]
        [Display(Name = "Register Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateArrive { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Status")]
        [MaxLength(20, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        public string? Status { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DataType(DataType.DateTime)]
        [Display(Name = "Register Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RegisterDate { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [Display(Name = "Register User")]
        [MaxLength(50, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        public string? RegisterUser { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [Display(Name = "Company")]
        [MaxLength(50, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        public string? Company { get; set; }

        public ICollection<XeroContainerLine>? Lines { get; set; }
    }
}
