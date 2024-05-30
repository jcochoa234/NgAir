using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using NgAir.FrontEnd.Repositories;
using NgAir.Shared.DTOs;

namespace NgAir.FrontEnd.Pages.Auth
{
    public partial class ResetPassword
    {
        private ResetPasswordDto resetPasswordDto = new();
        private bool loading;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Parameter, SupplyParameterFromQuery] public string Token { get; set; } = string.Empty;
        [CascadingParameter] IModalService Modal { get; set; } = default!;

        private async Task ChangePasswordAsync()
        {
            resetPasswordDto.Token = Token;
            loading = true;
            var responseHttp = await Repository.PostAsync("/api/accounts/ResetPassword", resetPasswordDto);
            loading = false;
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                loading = false;
                return;
            }

            await SweetAlertService.FireAsync("Confirmation", "Password successfully changed, now you can login with your new password.", SweetAlertIcon.Info);
            Modal.Show<Login>();
        }
    }
}