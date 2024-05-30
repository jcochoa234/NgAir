using NgAir.BackEnd.Paging;
using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;
using NgAir.Shared.Responses;

namespace NgAir.BackEnd.Repositories.Interfaces
{
    public interface IProductsRepository
    {
        Task<ActionResponse<Product>> AddFullAsync(ProductDto productDto);

        Task<ActionResponse<ImageDto>> AddImageAsync(ImageDto imageDto);

        Task<ActionResponse<Product>> DeleteAsync(int id);

        Task<ActionResponse<Product>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Product>>> GetAsync(RequestParams requestParams);

        Task<ActionResponse<PagingResponse<Product>>> GetPagedAsync(RequestParams requestParams);

        Task<ActionResponse<int>> GetTotalPagesAsync(RequestParams requestParams);

        Task<ActionResponse<ImageDto>> RemoveLastImageAsync(ImageDto imageDto);

        Task<ActionResponse<Product>> UpdateFullAsync(ProductDto productDto);

    }
}
