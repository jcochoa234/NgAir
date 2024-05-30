﻿namespace NgAir.Shared.DTOs
{
    public class TemporalOrderDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public float Quantity { get; set; } = 1;

        public string Remarks { get; set; } = string.Empty;
    }
}
