using Microsoft.EntityFrameworkCore;
using NgAir.BackEnd.Data;
using NgAir.BackEnd.Helpers;
using NgAir.BackEnd.Paging;
using NgAir.BackEnd.Repositories.Interfaces;
using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;
using NgAir.Shared.Responses;
using System.Linq.Expressions;
using System.Text.Json;

namespace NgAir.BackEnd.Repositories.Implementations
{
    public class ProductsRepository(DataContext context, IFileStorage fileStorage) : GenericRepository<Product>(context), IProductsRepository
    {
        private readonly DataContext _context = context;
        private readonly IFileStorage _fileStorage = fileStorage;

        public async Task<ActionResponse<Product>> AddFullAsync(ProductDto productDto)
        {
            try
            {
                var newProduct = new Product
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    Stock = productDto.Stock,
                    ProductCategories = new List<ProductCategory>(),
                    ProductImages = new List<ProductImage>()
                };

                foreach (var productImage in productDto.ProductImages!)
                {
                    var photoProduct = Convert.FromBase64String(productImage);
                    newProduct.ProductImages.Add(new ProductImage { Image = await _fileStorage.SaveFileAsync(photoProduct, ".jpg", "products") });
                }

                foreach (var productCategoryId in productDto.ProductCategoryIds!)
                {
                    var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == productCategoryId);
                    if (category != null)
                    {
                        newProduct.ProductCategories.Add(new ProductCategory { Category = category });
                    }
                }

                _context.Add(newProduct);
                await _context.SaveChangesAsync();
                return new ActionResponse<Product>
                {
                    WasSuccess = true,
                    Result = newProduct
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<Product>
                {
                    WasSuccess = false,
                    Message = "Ya existe un producto con el mismo nombre."
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Product>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }

        public async Task<ActionResponse<ImageDto>> AddImageAsync(ImageDto imageDto)
        {
            var product = await _context.Products
                .Include(x => x.ProductImages)
                .FirstOrDefaultAsync(x => x.Id == imageDto.ProductId);
            if (product == null)
            {
                return new ActionResponse<ImageDto>
                {
                    WasSuccess = false,
                    Message = "Producto no existe"
                };
            }

            for (int i = 0; i < imageDto.Images.Count; i++)
            {
                if (!imageDto.Images[i].StartsWith("https://"))
                {
                    var photoProduct = Convert.FromBase64String(imageDto.Images[i]);
                    imageDto.Images[i] = await _fileStorage.SaveFileAsync(photoProduct, ".jpg", "products");
                    product.ProductImages!.Add(new ProductImage { Image = imageDto.Images[i] });
                }
            }

            _context.Update(product);
            await _context.SaveChangesAsync();
            return new ActionResponse<ImageDto>
            {
                WasSuccess = true,
                Result = imageDto
            };
        }

        public override async Task<ActionResponse<Product>> DeleteAsync(int id)
        {
            var product = await _context.Products
                .Include(x => x.ProductCategories)
                .Include(x => x.ProductImages)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return new ActionResponse<Product>
                {
                    WasSuccess = false,
                    Message = "Producto no encontrado"
                };
            }

            foreach (var productImage in product.ProductImages!)
            {
                await _fileStorage.RemoveFileAsync(productImage.Image, "products");
            }

            try
            {
                _context.ProductCategories.RemoveRange(product.ProductCategories!);
                _context.ProductImages.RemoveRange(product.ProductImages!);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return new ActionResponse<Product>
                {
                    WasSuccess = true,
                };
            }
            catch
            {
                return new ActionResponse<Product>
                {
                    WasSuccess = false,
                    Message = "No se puede borrar el producto, porque tiene registros relacionados"
                };
            }
        }

        public override async Task<ActionResponse<Product>> GetAsync(int id)
        {
            var product = await _context.Products
                .Include(x => x.ProductImages)
                .Include(x => x.ProductCategories!)
                .ThenInclude(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return new ActionResponse<Product>
                {
                    WasSuccess = false,
                    Message = "Producto no existe"
                };
            }

            return new ActionResponse<Product>
            {
                WasSuccess = true,
                Result = product
            };
        }

        public override async Task<ActionResponse<IEnumerable<Product>>> GetAsync(RequestParams requestParams)
        {
            var queryable = _context.Products
                .Include(x => x.ProductImages)
                .Include(x => x.ProductCategories)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(requestParams.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(requestParams.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Product>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(requestParams)
                    .ToListAsync()
            };
        }

        public override Task<ActionResponse<PagingResponse<Product>>> GetPagedAsync(RequestParams requestParams)
        {
            Expression<Func<Product, bool>>? filters = null;

            List<ColumnFilter> columnFilters = [];
            if (!String.IsNullOrEmpty(requestParams.ColumnFilters))
            {
                try
                {
                    columnFilters.AddRange(JsonSerializer.Deserialize<List<ColumnFilter>>(requestParams.ColumnFilters)!);
                }
                catch (Exception)
                {
                    columnFilters = [];
                }
            }
            if (columnFilters.Count > 0)
            {
                var customFilter = CustomExpressionFilter<Product>.CustomFilter(columnFilters, "product");
                filters = customFilter;
            }

            var queryable = _context.Products.AsQueryable().FilterDynamic(filters!);

            var page = PagedList<Product>.ToPagedList(queryable.OrderByDynamic(requestParams), requestParams);

            var pagingResponse = new PagingResponse<Product>
            {
                Current = page.MetaData.Current,
                PageSize = page.MetaData.PageSize,
                Total = page.MetaData.Total,
                TotalPages = page.MetaData.TotalPages,
                Items = page.ToList(),
            };

            return Task.FromResult(new ActionResponse<PagingResponse<Product>>
            {
                WasSuccess = true,
                Result = pagingResponse
            });
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(RequestParams requestParams)
        {
            var queryable = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(requestParams.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(requestParams.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / requestParams.PageSize);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

        public async Task<ActionResponse<ImageDto>> RemoveLastImageAsync(ImageDto imageDto)
        {
            var product = await _context.Products
                .Include(x => x.ProductImages)
                .FirstOrDefaultAsync(x => x.Id == imageDto.ProductId);
            if (product == null)
            {
                return new ActionResponse<ImageDto>
                {
                    WasSuccess = false,
                    Message = "Producto no existe"
                };
            }

            if (product.ProductImages is null || product.ProductImages.Count == 0)
            {
                return new ActionResponse<ImageDto>
                {
                    WasSuccess = true,
                    Result = imageDto
                };
            }

            var lastImage = product.ProductImages.LastOrDefault();
            await _fileStorage.RemoveFileAsync(lastImage!.Image, "products");
            _context.ProductImages.Remove(lastImage);

            await _context.SaveChangesAsync();
            imageDto.Images = product.ProductImages.Select(x => x.Image).ToList();
            return new ActionResponse<ImageDto>
            {
                WasSuccess = true,
                Result = imageDto
            };
        }

        public async Task<ActionResponse<Product>> UpdateFullAsync(ProductDto productDto)
        {
            try
            {
                var product = await _context.Products
                    .Include(x => x.ProductCategories!)
                    .ThenInclude(x => x.Category)
                    .FirstOrDefaultAsync(x => x.Id == productDto.Id);
                if (product == null)
                {
                    return new ActionResponse<Product>
                    {
                        WasSuccess = false,
                        Message = "Producto no existe"
                    };
                }

                product.Name = productDto.Name;
                product.Description = productDto.Description;
                product.Price = productDto.Price;
                product.Stock = productDto.Stock;

                _context.ProductCategories.RemoveRange(product.ProductCategories!);
                product.ProductCategories = new List<ProductCategory>();

                foreach (var productCategoryId in productDto.ProductCategoryIds!)
                {
                    var category = await _context.Categories.FindAsync(productCategoryId);
                    if (category != null)
                    {
                        _context.ProductCategories.Add(new ProductCategory { CategoryId = category.Id, ProductId = product.Id });
                    }
                }

                _context.Update(product);
                await _context.SaveChangesAsync();
                return new ActionResponse<Product>
                {
                    WasSuccess = true,
                    Result = product
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<Product>
                {
                    WasSuccess = false,
                    Message = "Ya existe un producto con el mismo nombre."
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Product>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }

    }
}
