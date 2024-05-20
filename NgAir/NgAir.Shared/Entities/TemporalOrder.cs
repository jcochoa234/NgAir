using System.ComponentModel.DataAnnotations;

namespace NgAir.Shared.Entities
{
    public class TemporalOrder
    {
        public int Id { get; set; }

        public User? User { get; set; }

        public string? UserId { get; set; }

        public Product? Product { get; set; }

        public int ProductId { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public float Quantity { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Remarks")]
        public string? Remarks { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value => Product == null ? 0 : Product.Price * (decimal)Quantity;
    }
}
