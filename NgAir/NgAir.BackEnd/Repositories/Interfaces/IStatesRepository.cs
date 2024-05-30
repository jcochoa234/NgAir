using NgAir.BackEnd.Paging;
using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;
using NgAir.Shared.Responses;

namespace NgAir.BackEnd.Repositories.Interfaces
{
    public interface IStatesRepository
    {
        Task<ActionResponse<IEnumerable<State>>> GetAsync();

        Task<ActionResponse<State>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<State>>> GetAsync(RequestParams requestParams);

        Task<IEnumerable<State>> GetComboAsync(int countryId);

        Task<ActionResponse<PagingResponse<State>>> GetPagedAsync(RequestParams requestParams);

        Task<ActionResponse<int>> GetTotalPagesAsync(RequestParams requestParams);

    }
}
