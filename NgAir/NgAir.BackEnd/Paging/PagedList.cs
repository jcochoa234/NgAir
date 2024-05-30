using NgAir.Shared.DTOs;

namespace NgAir.BackEnd.Paging
{
    public class PagedList<T> : List<T>
    {
        public MetaData MetaData { get; set; }
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                Current = pageNumber,
                PageSize = pageSize,
                Total = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
            };
            AddRange(items);
        }
        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source
              .Skip((pageNumber - 1) * pageSize)
              .Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
        public static PagedList<T> ToPagedList(IEnumerable<T> source, RequestParams requestParams)
        {
            var count = source.Count();
            var items = source
              .Skip((requestParams.PageNumber - 1) * requestParams.PageSize)
              .Take(requestParams.PageSize).ToList();
            return new PagedList<T>(items, count, requestParams.PageNumber, requestParams.PageSize);
        }
    }
}
