using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;
using NgAir.Shared.Responses;

namespace NgAir.BackEnd.Repositories.Interfaces
{
    public interface IStatesRepository
    {
        Task<ActionResponse<IEnumerable<State>>> GetAsync();

        Task<ActionResponse<State>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<State>>> GetAsync(PaginationDTO pagination);

        Task<IEnumerable<State>> GetComboAsync(int countryId);

        Task<ActionResponse<IEnumerable<State>>> GetPagedAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

    }
}
