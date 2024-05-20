using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;
using NgAir.Shared.Responses;

namespace NgAir.BackEnd.Repositories.Interfaces
{
    public interface ICountriesRepository
    {
        Task<ActionResponse<Country>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Country>>> GetAsync();

        Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<IEnumerable<Country>> GetComboAsync();
    }
}
