using NgAir.BackEnd.Paging;
using NgAir.BackEnd.Repositories.Interfaces;
using NgAir.BackEnd.UnitsOfWork.Interfaces;
using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;
using NgAir.Shared.Responses;

namespace NgAir.BackEnd.UnitsOfWork.Implementations
{
    public class ProductsUnitOfWork : GenericUnitOfWork<Product>, IProductsUnitOfWork
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsUnitOfWork(IGenericRepository<Product> repository, IProductsRepository productsRepository) : base(repository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<ActionResponse<Product>> AddFullAsync(ProductDto productDto) => await _productsRepository.AddFullAsync(productDto);

        public async Task<ActionResponse<ImageDto>> AddImageAsync(ImageDto imageDto) => await _productsRepository.AddImageAsync(imageDto);

        public override async Task<ActionResponse<Product>> DeleteAsync(int id) => await _productsRepository.DeleteAsync(id);

        public override async Task<ActionResponse<Product>> GetAsync(int id) => await _productsRepository.GetAsync(id);

        public override async Task<ActionResponse<IEnumerable<Product>>> GetAsync(RequestParams requestParams) => await _productsRepository.GetAsync(requestParams);

        public async Task<ActionResponse<PagingResponse<Product>>> GetPagedAsync(RequestParams requestParams) => await _productsRepository.GetPagedAsync(requestParams);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(RequestParams requestParams) => await _productsRepository.GetTotalPagesAsync(requestParams);

        public async Task<ActionResponse<ImageDto>> RemoveLastImageAsync(ImageDto imageDto) => await _productsRepository.RemoveLastImageAsync(imageDto);

        public async Task<ActionResponse<Product>> UpdateFullAsync(ProductDto productDto) => await _productsRepository.UpdateFullAsync(productDto);

    }
}
