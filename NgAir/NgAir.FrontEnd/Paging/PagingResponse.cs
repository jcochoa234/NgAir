﻿namespace NgAir.FrontEnd.Paging
{
    public class PagingResponse<T> where T : class
    {
        public int Current { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public int TotalPages { get; set; }

        public List<T> Items { get; set; }
    }
}
