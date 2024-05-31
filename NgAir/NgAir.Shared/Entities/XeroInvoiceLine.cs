using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NgAir.Shared.Entities
{
    [Table("XeroInvoiceLines")]
    public class XeroInvoiceLine
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Invoice ID")]
        [MaxLength(50, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        public required string InvoiceID { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Invoice Number")]
        [MaxLength(50, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        public required string InvoiceNumber { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Item ID")]
        [MaxLength(50, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        public required string LineItemID { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        public decimal LineAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [Display(Name = "Quantity")]
        [DataType(DataType.Currency)]
        public decimal Quantity { get; set; }

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
        [Display(Name = "Unit Amount")]
        [DataType(DataType.Currency)]
        public decimal UnitAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [Display(Name = "Discount Amount")]
        [DataType(DataType.Currency)]
        public decimal DiscountAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [Display(Name = "Discount Rate")]
        [DataType(DataType.Currency)]
        public decimal DiscountRate { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [Display(Name = "Tax Amount")]
        [DataType(DataType.Currency)]
        public decimal TaxAmount { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Account Id")]
        [MaxLength(50, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        public required string AccountId { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Account Code")]
        [MaxLength(150, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        public required string AccountCode { get; set; }

        public int XeroInvoiceHeaderId { get; set; }

        public XeroInvoiceHeader? XeroInvoiceHeader { get; set; }

        public ICollection<XeroInvoiceLineSerie>? Series { get; set; }
    }
}
