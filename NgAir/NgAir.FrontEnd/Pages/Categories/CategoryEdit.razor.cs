using AntDesign;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using NgAir.FrontEnd.Repositories;
using NgAir.FrontEnd.Shared;
using NgAir.Shared.Entities;
using System.Net;

namespace NgAir.FrontEnd.Pages.Categories
{
    [Authorize(Roles = "Admin")]
    public partial class CategoryEdit
    {
        private Category? Category;
        private FormWithName<Category>? categoryForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ModalService ModalService { get; set; } = null!;
        [Inject] private IMessageService Message { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;


        [EditorRequired, Parameter] public int Id { get; set; }

        [EditorRequired, Parameter] public int paramenter2 { get; set; }

        [EditorRequired, Parameter] public string paramenter { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            var responseHttp = await Repository.GetAsync<Category>($"/api/categories/{Id}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/categories");
                }
                else
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    await ModalService.ErrorAsync(new ConfirmOptions
                    {
                        Title = "Error",
                        Content = message,
                        OkText = "Close"
                    });
                }
            }
            else
            {
                Category = responseHttp.Response;
            }
        }

        private async Task EditAsync()
        {
            var responseHttp = await Repository.PutAsync("/api/categories", Category);
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

            Return();

            await Message.Success("Successfully saved changes.");

        }

        private void Return()
        {
            categoryForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/categories");
        }
    }
}