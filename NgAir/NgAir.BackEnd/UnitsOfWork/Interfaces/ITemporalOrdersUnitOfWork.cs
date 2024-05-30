using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;
using NgAir.Shared.Responses;

namespace NgAir.BackEnd.UnitsOfWork.Interfaces
{
    public interface ITemporalOrdersUnitOfWork
    {
        Task<ActionResponse<TemporalOrderDto>> AddFullAsync(string email, TemporalOrderDto temporalOrderDto);

        Task<ActionResponse<TemporalOrder>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<TemporalOrder>>> GetAsync(string email);

        Task<ActionResponse<int>> GetCountAsync(string email);

        Task<ActionResponse<TemporalOrder>> PutFullAsync(TemporalOrderDto temporalOrderDto);

    }
}
