using NgAir.BackEnd.Paging;
using NgAir.Shared.DTOs;
using NgAir.Shared.Responses;

namespace NgAir.BackEnd.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<ActionResponse<T>> AddAsync(T entity);

        Task<ActionResponse<T>> DeleteAsync(int id);

        Task<ActionResponse<IEnumerable<T>>> GetAsync();

        Task<ActionResponse<T>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<T>>> GetAsync(RequestParams requestParams);

        Task<ActionResponse<PagingResponse<T>>> GetPagedAsync(RequestParams requestParams);

        Task<ActionResponse<int>> GetTotalPagesAsync(RequestParams requestParams);

        Task<ActionResponse<T>> UpdateAsync(T entity);

    }
}
