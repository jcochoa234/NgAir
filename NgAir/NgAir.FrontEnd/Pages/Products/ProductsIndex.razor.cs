using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using NgAir.FrontEnd.Repositories;
using NgAir.Shared.Entities;

namespace NgAir.FrontEnd.Pages.Products
{
    [Authorize(Roles = "Admin")]
    public partial class ProductsIndex
    {
        private int currentPage = 1;
        private int totalPages;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        public List<Product>? Products { get; set; }

        [Parameter, SupplyParameterFromQuery] public string PageNumber { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public int PageSize { get; set; } = 10;

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task SelectedPageSizeAsync(int pageSize)
        {
            PageSize = pageSize;
            int pageNumber = 1;
            await LoadAsync(pageNumber);
            await SelectedPageNumberAsync(pageNumber);
        }

        private async Task FilterCallBack(string filter)
        {
            Filter = filter;
            await ApplyFilterAsync();
            StateHasChanged();
        }

        private async Task SelectedPageNumberAsync(int pageNumber)
        {
            if (!string.IsNullOrWhiteSpace(PageNumber))
            {
                pageNumber = Convert.ToInt32(PageNumber);
            }

            currentPage = pageNumber;
            await LoadAsync(pageNumber);
        }

        private async Task LoadAsync(int pageNumber = 1)
        {
            if (!string.IsNullOrWhiteSpace(PageNumber))
            {
                pageNumber = Convert.ToInt32(PageNumber);
            }

            var ok = await LoadListAsync(pageNumber);
            if (ok)
            {
                await LoadPagesAsync();
            }
        }

        private void ValidatePageSize(int pageSize)
        {
            if (pageSize == 0)
            {
                PageSize = 10;
            }
        }

        private async Task<bool> LoadListAsync(int pageNumber)
        {
            ValidatePageSize(PageSize);
            var url = $"api/products?pageNumber={pageNumber}&PageSize={PageSize}";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }

            var response = await Repository.GetAsync<List<Product>>(url);
            if (response.Error)
            {
                var message = await response.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            Products = response.Response;
            return true;
        }

        private async Task LoadPagesAsync()
        {
            ValidatePageSize(PageSize);
            var url = $"api/products/totalPages?pageSize={PageSize}";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }

            var response = await Repository.GetAsync<int>(url);
            if (response.Error)
            {
                var message = await response.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            totalPages = response.Response;
        }

        private async Task Delete(int productId)
        {
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmation",
                Text = "Are you sure you want to delete the record?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true
            });
            var confirm = string.IsNullOrEmpty(result.Value);

            if (confirm)
            {
                return;
            }

            var responseHttp = await Repository.DeleteAsync<Product>($"api/products/{productId}");

            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/");
                    return;
                }

                var mensajeError = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
                return;
            }

            await LoadAsync(1);
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Record successfully deleted.");
        }

        private async Task ApplyFilterAsync()
        {
            int pageNumber = 1;
            await LoadAsync(pageNumber);
            await SelectedPageNumberAsync(pageNumber);
        }
    }
}