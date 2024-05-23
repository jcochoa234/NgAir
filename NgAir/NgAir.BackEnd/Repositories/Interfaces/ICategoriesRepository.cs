using NgAir.BackEnd.Paging;
using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;
using NgAir.Shared.Responses;

namespace NgAir.BackEnd.Repositories.Interfaces
{
    public interface ICategoriesRepository
    {
        Task<ActionResponse<IEnumerable<Category>>> GetAsync(PaginationDTO pagination);

        Task<IEnumerable<Category>> GetComboAsync();

        Task<ActionResponse<PagingResponse<Category>>> GetPagedAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

    }
}
