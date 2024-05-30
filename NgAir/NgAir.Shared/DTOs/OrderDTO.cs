using NgAir.Shared.Enums;

namespace NgAir.Shared.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string Remarks { get; set; } = string.Empty;
    }
}
