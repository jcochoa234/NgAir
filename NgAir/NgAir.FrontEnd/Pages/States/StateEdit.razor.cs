using AntDesign;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using NgAir.FrontEnd.Repositories;
using NgAir.FrontEnd.Shared;
using NgAir.Shared.Entities;
using System.Net;

namespace NgAir.FrontEnd.Pages.States
{
    [Authorize(Roles = "Admin")]
    public partial class StateEdit
    {
        private State? State;
        private FormWithName<State>? stateForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ModalService ModalService { get; set; } = null!;
        [Inject] private IMessageService Message { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Parameter] public int StateId { get; set; }
        [Parameter] public EventCallback<string> OnSave { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            var responseHttp = await Repository.GetAsync<State>($"/api/states/{StateId}");
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
            State = responseHttp.Response;
        }

        private async Task SaveAsync()
        {
            var responseHttp = await Repository.PutAsync($"/api/states", State);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
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
            stateForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/countries/details/{State!.CountryId}");
        }
    }
}