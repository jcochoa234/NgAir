using NgAir.BackEnd.Paging;
using NgAir.BackEnd.Repositories.Interfaces;
using NgAir.BackEnd.UnitsOfWork.Interfaces;
using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;
using NgAir.Shared.Responses;

namespace NgAir.BackEnd.UnitsOfWork.Implementations
{
    public class CategoriesUnitOfWork : GenericUnitOfWork<Category>, ICategoriesUnitOfWork
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesUnitOfWork(IGenericRepository<Category> repository, ICategoriesRepository categoriesRepository) : base(repository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<ActionResponse<IEnumerable<Category>>> GetAsync(RequestParams requestParams) => await _categoriesRepository.GetAsync(requestParams);

        public async Task<IEnumerable<Category>> GetComboAsync() => await _categoriesRepository.GetComboAsync();

        public async Task<ActionResponse<PagingResponse<Category>>> GetPagedAsync(RequestParams requestParams) => await _categoriesRepository.GetPagedAsync(requestParams);

        public async Task<ActionResponse<int>> GetTotalPagesAsync(RequestParams requestParams) => await _categoriesRepository.GetTotalPagesAsync(requestParams);

    }
}
