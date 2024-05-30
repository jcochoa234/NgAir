using NgAir.Shared.DTOs;
using System.Linq.Expressions;

namespace NgAir.BackEnd.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, RequestParams requestParams)
        {
            return queryable
                .Skip((requestParams.PageNumber - 1) * requestParams.PageSize)
                .Take(requestParams.PageSize);
        }

        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> queryable, RequestParams requestParams)
        {
            var queryElementTypeParam = Expression.Parameter(typeof(T));
            var memberAccess = Expression.PropertyOrField(queryElementTypeParam, requestParams.SortField!);

            var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

            var orderBy = Expression.Call(
                typeof(Queryable),
                requestParams.SortOrder == "ascend" ? "OrderBy" : "OrderByDescending",
                new Type[] { typeof(T), memberAccess.Type },
                queryable.Expression,
                Expression.Quote(keySelector));

            return queryable.Provider.CreateQuery<T>(orderBy);
        }

        public static IQueryable<T> FilterDynamic<T>(this IQueryable<T> queryable, Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                queryable = queryable.Where(filter);
            }

            return queryable;
        }
    }
}
