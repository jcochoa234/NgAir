namespace NgAir.Shared.DTOs
{
    public class PaginationDTO
    {
        public int Id { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? Filter { get; set; }
    }
}
