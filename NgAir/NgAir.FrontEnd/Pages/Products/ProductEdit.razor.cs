using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using NgAir.FrontEnd.Repositories;
using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;

namespace NgAir.FrontEnd.Pages.Products
{
    [Authorize(Roles = "Admin")]
    public partial class ProductEdit
    {
        private ProductDto productDto = new()
        {
            ProductCategoryIds = new List<int>(),
            ProductImages = new List<string>()
        };

        private ProductForm? productForm;
        private List<Category> selectedCategories = new();
        private List<Category> nonSelectedCategories = new();
        private bool loading = true;
        private Product? product;
        [Parameter] public int ProductId { get; set; }
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await LoadProductAsync();
            await LoadCategoriesAsync();
        }

        private async Task AddImageAsync()
        {
            if (productDto.ProductImages is null || productDto.ProductImages.Count == 0)
            {
                return;
            }

            var imageDto = new ImageDto
            {
                ProductId = ProductId,
                Images = productDto.ProductImages!
            };

            var httpActionResponse = await Repository.PostAsync<ImageDto, ImageDto>("/api/products/addImages", imageDto);
            if (httpActionResponse.Error)
            {
                var message = await httpActionResponse.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            productDto.ProductImages = httpActionResponse.Response!.Images;
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Images successfully added.");
        }

        private async Task RemoveImageAsyc()
        {
            if (productDto.ProductImages is null || productDto.ProductImages.Count == 0)
            {
                return;
            }

            var imageDto = new ImageDto
            {
                ProductId = ProductId,
                Images = productDto.ProductImages!
            };

            var httpActionResponse = await Repository.PostAsync<ImageDto, ImageDto>("/api/products/removeLastImage", imageDto);
            if (httpActionResponse.Error)
            {
                var message = await httpActionResponse.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            productDto.ProductImages = httpActionResponse.Response!.Images;
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Image successfully deleted.");
        }

        private async Task LoadProductAsync()
        {
            loading = true;
            var httpActionResponse = await Repository.GetAsync<Product>($"/api/products/{ProductId}");

            if (httpActionResponse.Error)
            {
                loading = false;
                var message = await httpActionResponse.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            product = httpActionResponse.Response!;
            productDto = ToProductDto(product);
            loading = false;
        }

        private ProductDto ToProductDto(Product product)
        {
            return new ProductDto
            {
                Description = product.Description,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                ProductCategoryIds = product.ProductCategories!.Select(x => x.CategoryId).ToList(),
                ProductImages = product.ProductImages!.Select(x => x.Image).ToList()
            };
        }

        private async Task LoadCategoriesAsync()
        {
            loading = true;
            var httpActionResponse = await Repository.GetAsync<List<Category>>("/api/categories/combo");

            if (httpActionResponse.Error)
            {
                loading = false;
                var message = await httpActionResponse.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            var categories = httpActionResponse.Response!;
            foreach (var category in categories!)
            {
                var found = product!.ProductCategories!.FirstOrDefault(x => x.CategoryId == category.Id);
                if (found == null)
                {
                    nonSelectedCategories.Add(category);
                }
                else
                {
                    selectedCategories.Add(category);
                }
            }
            loading = false;
        }

        private async Task SaveChangesAsync()
        {
            var httpActionResponse = await Repository.PutAsync("/api/products/full", productDto);
            if (httpActionResponse.Error)
            {
                var message = await httpActionResponse.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            Return();
        }

        private void Return()
        {
            productForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/products");
        }
    }
}