using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NgAir.Shared.Entities
{
    [Table("XeroContainerLineSeries")]
    public class XeroContainerLineSerie
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Container ID")]
        [MaxLength(50, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        public required string ContainerNumber { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Item Code")]
        [MaxLength(50, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        public required string ItemCode { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Description")]
        [MaxLength(350, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Serial Number")]
        [MaxLength(50, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        public required string SerialNumber { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DataType(DataType.DateTime)]
        [Display(Name = "Register Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RegisterDate { get; set; }

        public int XeroContainerLineId { get; set; }

        public XeroContainerLine? XeroContainerLine { get; set; }
    }
}
