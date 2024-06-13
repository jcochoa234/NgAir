using AntDesign;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using NgAir.FrontEnd.Repositories;
using NgAir.FrontEnd.Shared;
using NgAir.Shared.Entities;

namespace NgAir.FrontEnd.Pages.Categories
{
    [Authorize(Roles = "Admin")]
    public partial class CategoryCreate
    {
        private Category Category = new();
        private FormWithName<Category>? categoryForm;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ModalService ModalService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Parameter] public EventCallback<string> OnSave { get; set; }

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/categories", Category);
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

            await ModalService.SuccessAsync(new ConfirmOptions
            {
                Title = "Success",
                Content = "Registration successfully created.",
                OkText = "Close",
            });

        }

        private void Return()
        {
            categoryForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/categories");
        }
    }
}