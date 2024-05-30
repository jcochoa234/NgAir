using NgAir.BackEnd.Paging;
using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;
using NgAir.Shared.Responses;

namespace NgAir.BackEnd.Repositories.Interfaces
{
    public interface ICountriesRepository
    {
        Task<ActionResponse<IEnumerable<Country>>> GetAsync();

        Task<ActionResponse<Country>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Country>>> GetAsync(RequestParams requestParams);

        Task<IEnumerable<Country>> GetComboAsync();

        Task<ActionResponse<PagingResponse<Country>>> GetPagedAsync(RequestParams requestParams);

        Task<ActionResponse<int>> GetTotalPagesAsync(RequestParams requestParams);

    }
}
