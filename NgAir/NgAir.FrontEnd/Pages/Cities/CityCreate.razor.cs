
using AntDesign;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using NgAir.FrontEnd.Repositories;
using NgAir.FrontEnd.Shared;
using NgAir.Shared.Entities;

namespace NgAir.FrontEnd.Pages.Cities
{
    [Authorize(Roles = "Admin")]
    public partial class CityCreate
    {
        private City City = new();
        private FormWithName<City>? cityForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ModalService ModalService { get; set; } = null!;
        [Inject] private IMessageService Message { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Parameter] public int StateId { get; set; }
        [Parameter] public EventCallback<string> OnSave { get; set; }

        protected override Task OnInitializedAsync()
        {
            return Task.CompletedTask;
        }

        private async Task CreateAsync()
        {
            City.StateId = StateId;
            var responseHttp = await Repository.PostAsync("/api/cities", City);
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
            cityForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/states/details/{StateId}");
        }
    }
}