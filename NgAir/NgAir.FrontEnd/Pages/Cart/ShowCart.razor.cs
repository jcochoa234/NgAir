using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using NgAir.FrontEnd.Repositories;
using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;

namespace NgAir.FrontEnd.Pages.Cart
{
    [Authorize(Roles = "Admin, User")]
    public partial class ShowCart
    {
        public List<TemporalOrder>? temporalOrders { get; set; }
        private float sumQuantity;
        private decimal sumValue;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        public OrderDto OrderDto { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            try
            {
                var responseHppt = await Repository.GetAsync<List<TemporalOrder>>("api/temporalOrders/my");
                temporalOrders = responseHppt.Response!;
                sumQuantity = temporalOrders.Sum(x => x.Quantity);
                sumValue = temporalOrders.Sum(x => x.Value);
            }
            catch (Exception ex)
            {
                await SweetAlertService.FireAsync("Error", ex.Message, SweetAlertIcon.Error);
            }
        }

        private void ConfirmOrderAsync()
        {
            //TODO: Pending to implement
        }

        private async Task Delete(int temporalOrderId)
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

            var responseHttp = await Repository.DeleteAsync<TemporalOrder>($"api/temporalOrders/{temporalOrderId}");

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

            await LoadAsync();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = false,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Product removed from shopping cart.");
        }
    }
}