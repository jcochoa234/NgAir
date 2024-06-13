using AntDesign;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using NgAir.FrontEnd.Repositories;
using NgAir.FrontEnd.Shared;
using NgAir.Shared.Entities;
using System.Net;

namespace NgAir.FrontEnd.Pages.Cities
{
    [Authorize(Roles = "Admin")]
    public partial class CityEdit
    {
        private City? City;
        private FormWithName<City>? cityForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ModalService ModalService { get; set; } = null!;
        [Inject] private IMessageService Message { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Parameter] public int CityId { get; set; }
        [Parameter] public EventCallback<string> OnSave { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            var responseHttp = await Repository.GetAsync<City>($"/api/cities/{CityId}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    Return();
                }
                var message = await responseHttp.GetErrorMessageAsync();
                await ModalService.ErrorAsync(new ConfirmOptions
                {
                    Title = "Error",
                    Content = message,
                    OkText = "Close"
                });
                return;
            }
            City = responseHttp.Response;
        }

        private async Task SaveAsync()
        {
            var response = await Repository.PutAsync($"/api/cities", City);
            if (response.Error)
            {
                var message = await response.GetErrorMessageAsync();
                await ModalService.ErrorAsync(new ConfirmOptions
                {
                    Title = "Error",
                    Content = message,
                    OkText = "Close"
                });
                return;
            }

            if (OnSave.HasDelegate)
            {
                await OnSave.InvokeAsync("Correct");
            }

            Return();

            await Message.Success("Successfully saved changes.");
        }

        private void Return()
        {
            cityForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/states/details/{City!.StateId}");
        }
    }
}