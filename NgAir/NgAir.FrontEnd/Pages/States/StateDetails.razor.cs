using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using NgAir.FrontEnd.Pages.Cities;
using NgAir.FrontEnd.Repositories;
using NgAir.Shared.Entities;
using System.Net;

namespace NgAir.FrontEnd.Pages.States
{
    [Authorize(Roles = "Admin")]
    public partial class StateDetails
    {
        private State? state;
        private List<City>? cities;
        private int currentPage = 1;
        private int totalPages;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        [Parameter] public int StateId { get; set; }
        [Parameter, SupplyParameterFromQuery] public string PageNumber { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public int PageSize { get; set; } = 10;
        [CascadingParameter] IModalService Modal { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task ShowModalAsync(int id = 0, bool isEdit = false)
        {
            IModalReference modalReference;

            if (isEdit)
            {
                modalReference = Modal.Show<CityEdit>(string.Empty, new ModalParameters().Add("CityId", id));
            }
            else
            {
                modalReference = Modal.Show<CityCreate>(string.Empty, new ModalParameters().Add("StateId", StateId));
            }

            var result = await modalReference.Result;
            if (result.Confirmed)
            {
                await LoadAsync();
            }
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
            var ok = await LoadStateAsync();
            if (ok)
            {
                ok = await LoadCitiesAsync(pageNumber);
                if (ok)
                {
                    await LoadPagesAsync();
                }
            }
        }

        private void ValidatePageSize()
        {
            if (PageSize == 0)
            {
                PageSize = 10;
            }
        }

        private async Task LoadPagesAsync()
        {
            ValidatePageSize();
            var url = $"api/cities/totalPages?id={StateId}&PageSize={PageSize}";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }

            var responseHttp = await Repository.GetAsync<int>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            totalPages = responseHttp.Response;
        }

        private async Task<bool> LoadCitiesAsync(int pageNumber)
        {
            ValidatePageSize();
            var url = $"api/cities?id={StateId}&pageNumber={pageNumber}&PageSize={PageSize}";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }

            var responseHttp = await Repository.GetAsync<List<City>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            cities = responseHttp.Response;
            return true;
        }

        private async Task ApplyFilterAsync()
        {
            int pageNumber = 1;
            await LoadAsync(pageNumber);
            await SelectedPageNumberAsync(pageNumber);
        }

        private async Task<bool> LoadStateAsync()
        {
            var responseHttp = await Repository.GetAsync<State>($"api/states/{StateId}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/countries");
                    return false;
                }

                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            state = responseHttp.Response;
            return true;
        }

        private async Task DeleteAsync(City city)
        {
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = $"¿Realmente deseas eliminar la ciudad? {city.Name}",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                CancelButtonText = "No",
                ConfirmButtonText = "Si"
            });

            var confirm = string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            var responseHttp = await Repository.DeleteAsync<City>($"/api/cities/{city.Id}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode != HttpStatusCode.NotFound)
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                    return;
                }
            }

            await LoadAsync();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro borrado con éxito.");
        }
    }
}