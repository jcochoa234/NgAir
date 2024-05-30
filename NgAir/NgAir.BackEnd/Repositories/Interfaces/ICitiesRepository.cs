using NgAir.BackEnd.Paging;
using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;
using NgAir.Shared.Responses;

namespace NgAir.BackEnd.Repositories.Interfaces
{
    public interface ICitiesRepository
    {
        Task<ActionResponse<IEnumerable<City>>> GetAsync(RequestParams requestParams);

        Task<IEnumerable<City>> GetComboAsync(int stateId);

        Task<ActionResponse<PagingResponse<City>>> GetPagedAsync(RequestParams requestParams);

        Task<ActionResponse<int>> GetTotalPagesAsync(RequestParams requestParams);

    }
}
