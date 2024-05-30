using NgAir.BackEnd.Paging;
using NgAir.BackEnd.Repositories.Interfaces;
using NgAir.BackEnd.UnitsOfWork.Interfaces;
using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;
using NgAir.Shared.Responses;

namespace NgAir.BackEnd.UnitsOfWork.Implementations
{
    public class CountriesUnitOfWork : GenericUnitOfWork<Country>, ICountriesUnitOfWork
    {
        private readonly ICountriesRepository _countriesRepository;

        public CountriesUnitOfWork(IGenericRepository<Country> repository, ICountriesRepository countriesRepository) : base(repository)
        {
            _countriesRepository = countriesRepository;
        }

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync() => await _countriesRepository.GetAsync();

        public override async Task<ActionResponse<Country>> GetAsync(int id) => await _countriesRepository.GetAsync(id);

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync(RequestParams requestParams) => await _countriesRepository.GetAsync(requestParams);

        public async Task<IEnumerable<Country>> GetComboAsync() => await _countriesRepository.GetComboAsync();

        public async Task<ActionResponse<PagingResponse<Country>>> GetPagedAsync(RequestParams requestParams) => await _countriesRepository.GetPagedAsync(requestParams);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(RequestParams requestParams) => await _countriesRepository.GetTotalPagesAsync(requestParams);

    }
}
