using NgAir.BackEnd.Paging;
using NgAir.BackEnd.Repositories.Interfaces;
using NgAir.BackEnd.UnitsOfWork.Interfaces;
using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;
using NgAir.Shared.Responses;

namespace NgAir.BackEnd.UnitsOfWork.Implementations
{
    public class CitiesUnitOfWork : GenericUnitOfWork<City>, ICitiesUnitOfWork
    {
        private readonly ICitiesRepository _citiesRepository;

        public CitiesUnitOfWork(IGenericRepository<City> repository, ICitiesRepository citiesRepository) : base(repository)
        {
            _citiesRepository = citiesRepository;
        }

        public override async Task<ActionResponse<IEnumerable<City>>> GetAsync(RequestParams requestParams) => await _citiesRepository.GetAsync(requestParams);

        public async Task<IEnumerable<City>> GetComboAsync(int stateId) => await _citiesRepository.GetComboAsync(stateId);

        public async Task<ActionResponse<PagingResponse<City>>> GetPagedAsync(RequestParams requestParams) => await _citiesRepository.GetPagedAsync(requestParams);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(RequestParams requestParams) => await _citiesRepository.GetTotalPagesAsync(requestParams);

    }
}
