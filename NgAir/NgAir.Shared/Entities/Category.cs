using NgAir.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NgAir.Shared.Entities
{
    public class Category : IEntityWithName
    {
        public int Id { get; set; }

        [Display(Name = "Category")]
        [MaxLength(100, ErrorMessage = "The maximun length for field {0} is {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Name { get; set; } = null!;

        public ICollection<ProductCategory>? ProductCategories { get; set; }

        [Display(Name = "Products")]
        public int ProductCategoriesNumber => ProductCategories == null || ProductCategories.Count == 0 ? 0 : ProductCategories.Count;
    }
}
