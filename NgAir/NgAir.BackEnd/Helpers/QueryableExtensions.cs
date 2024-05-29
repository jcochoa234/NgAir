using NgAir.Shared.DTOs;
using System.Linq.Expressions;

namespace NgAir.BackEnd.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO pagination)
        {
            return queryable
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize);
        }

        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> queryable, PaginationDTO pagination)
        {
            var queryElementTypeParam = Expression.Parameter(typeof(T));
            var memberAccess = Expression.PropertyOrField(queryElementTypeParam, pagination.SortField!);

            var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

            var orderBy = Expression.Call(
                typeof(Queryable),
                pagination.SortOrder == "ascend" ? "OrderBy" : "OrderByDescending",
                new Type[] { typeof(T), memberAccess.Type },
                queryable.Expression,
                Expression.Quote(keySelector));

            return queryable.Provider.CreateQuery<T>(orderBy);
        }
    }
}
