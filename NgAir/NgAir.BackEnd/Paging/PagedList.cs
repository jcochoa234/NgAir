namespace NgAir.BackEnd.Paging
{
    public class PagedList<T> : List<T>
    {
        public int Current { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            Current = pageNumber;
            PageSize = pageSize;
            Total = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }
        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source
              .Skip((pageNumber - 1) * pageSize)
              .Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
        //public static PagedList<T> ToPagedList(IEnumerable<T> source, PaginationDTO pagination)
        //{
        //    var count = source.Count();
        //    var items = source
        //      .Skip((pagination.PageNumber - 1) * pagination.PageSize)
        //      .Take(pagination.PageSize).ToList();
        //    return new PagedList<T>(items, count, pagination.PageNumber, pagination.PageSize);
        //}
    }
}
