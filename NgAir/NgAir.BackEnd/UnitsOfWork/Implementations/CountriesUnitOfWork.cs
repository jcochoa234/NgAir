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

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination) => await _countriesRepository.GetAsync(pagination);

        public async Task<IEnumerable<Country>> GetComboAsync() => await _countriesRepository.GetComboAsync();

        public async Task<ActionResponse<PagingResponse<Country>>> GetPagedAsync(PaginationDTO pagination) => await _countriesRepository.GetPagedAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _countriesRepository.GetTotalPagesAsync(pagination);

    }
}
