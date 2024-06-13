using AntDesign;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using NgAir.FrontEnd.Repositories;
using NgAir.FrontEnd.Shared;
using NgAir.Shared.Entities;

namespace NgAir.FrontEnd.Pages.States
{
    [Authorize(Roles = "Admin")]
    public partial class StateCreate
    {
        private State State = new();
        private FormWithName<State>? stateForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ModalService ModalService { get; set; } = null!;
        [Inject] private IMessageService Message { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Parameter] public int CountryId { get; set; }
        [Parameter] public EventCallback<string> OnSave { get; set; }

        private async Task CreateAsync()
        {
            State.CountryId = CountryId;
            var responseHttp = await Repository.PostAsync("/api/states", State);
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
            NavigationManager.NavigateTo($"/countries/details/{CountryId}");
        }
    }
}