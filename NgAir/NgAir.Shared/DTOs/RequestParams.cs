namespace NgAir.Shared.DTOs
{
    public class RequestParams : PaginationDto
    {
        public int? Id { get; set; }

        public string? ColumnFilters { get; set; }

        public string? SortField { get; set; }

        public string? SortOrder { get; set; }

        public string? Filter { get; set; }
    }
}
