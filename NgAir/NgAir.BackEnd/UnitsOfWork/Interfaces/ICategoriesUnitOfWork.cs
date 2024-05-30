using NgAir.BackEnd.Paging;
using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;
using NgAir.Shared.Responses;

namespace NgAir.BackEnd.UnitsOfWork.Interfaces
{
    public interface ICategoriesUnitOfWork
    {
        Task<ActionResponse<IEnumerable<Category>>> GetAsync(RequestParams requestParams);

        Task<IEnumerable<Category>> GetComboAsync();

        Task<ActionResponse<int>> GetTotalPagesAsync(RequestParams requestParams);

        Task<ActionResponse<PagingResponse<Category>>> GetPagedAsync(RequestParams requestParams);

    }
}
