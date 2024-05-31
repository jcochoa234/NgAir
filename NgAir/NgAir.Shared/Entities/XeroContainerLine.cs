using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NgAir.Shared.Entities
{
    [Table("XeroContainerLines")]
    public class XeroContainerLine
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

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [Display(Name = "Quantity")]
        [DataType(DataType.Currency)]
        public decimal Quantity { get; set; }

        public int XeroContainerHeaderId { get; set; }

        public XeroContainerHeader? XeroContainerHeader { get; set; }

        public ICollection<XeroContainerLineSerie>? Series { get; set; }
    }
}
