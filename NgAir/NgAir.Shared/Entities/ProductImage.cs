using System.ComponentModel.DataAnnotations;

namespace NgAir.Shared.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        public Product? Product { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; } = null!;
    }
}
