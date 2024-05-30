using System.ComponentModel.DataAnnotations;

namespace NgAir.Shared.DTOs
{
    public class ImageDto
    {
        [Required] public int ProductId { get; set; }

        [Required] public List<string> Images { get; set; } = null!;
    }
}
