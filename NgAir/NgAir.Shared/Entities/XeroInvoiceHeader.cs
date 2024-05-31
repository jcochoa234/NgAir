using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NgAir.Shared.Entities
{
    [Table("XeroInvoiceHeaders")]
    public class XeroInvoiceHeader
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Invoice ID")]
        [MaxLength(50, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        public string? InvoiceID { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Invoice Number")]
        [MaxLength(50, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        public string? InvoiceNumber { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Reference")]
        [MaxLength(150, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        public string? Reference { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [Display(Name = "Amount Due")]
        [DataType(DataType.Currency)]
        public decimal AmountDue { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [Display(Name = "Amount Paid")]
        [DataType(DataType.Currency)]
        public decimal AmountPaid { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [Display(Name = "Amount Credited")]
        [DataType(DataType.Currency)]
        public decimal AmountCredited { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [Display(Name = "Currency Rate")]
        [DataType(DataType.Currency)]
        public decimal CurrencyRate { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [Display(Name = "SubTotal")]
        [DataType(DataType.Currency)]
        public decimal SubTotal { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [Display(Name = "Total Tax")]
        [DataType(DataType.Currency)]
        public decimal TotalTax { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DataType(DataType.DateTime)]
        [Display(Name = "Due Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DataType(DataType.DateTime)]
        [Display(Name = "Update Date UTC")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UpdateDateUTC { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Currency Code")]
        [MaxLength(20, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        public string? CurrencyCode { get; set; }

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

        public int XeroContactId { get; set; }

        public XeroContact? XeroContact { get; set; }

        public ICollection<XeroInvoiceLine>? Lines { get; set; }
    }
}
