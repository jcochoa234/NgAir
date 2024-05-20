﻿using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;
using NgAir.Shared.Responses;

namespace NgAir.BackEnd.UnitsOfWork.Interfaces
{
    public interface IProductsUnitOfWork
    {
        Task<ActionResponse<Product>> DeleteAsync(int id);

        Task<ActionResponse<ImageDTO>> AddImageAsync(ImageDTO imageDTO);

        Task<ActionResponse<ImageDTO>> RemoveLastImageAsync(ImageDTO imageDTO);

        Task<ActionResponse<Product>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Product>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<ActionResponse<Product>> AddFullAsync(ProductDTO productDTO);

        Task<ActionResponse<Product>> UpdateFullAsync(ProductDTO productDTO);
    }
}