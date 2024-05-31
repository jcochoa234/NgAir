using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NgAir.Shared.Entities
{
    [Table("XeroInvoiceLineSeries")]
    public class XeroInvoiceLineSerie
    {
        [Key]
        public int Id { get; set; }

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

        public int XeroInvoiceLineId { get; set; }

        public XeroInvoiceLine? XeroInvoiceLine { get; set; }
    }
}
