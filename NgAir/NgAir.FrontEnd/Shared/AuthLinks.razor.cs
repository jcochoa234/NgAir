using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using NgAir.FrontEnd.Pages.Auth;

namespace NgAir.FrontEnd.Shared
{
    public partial class AuthLinks
    {
        private string? photoUser;

        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;
        [CascadingParameter] IModalService Modal { get; set; } = default!;


        protected override async Task OnParametersSetAsync()
        {
            var authenticationState = await AuthenticationStateTask;
            var claims = authenticationState.User.Claims.ToList();
            var photoClaim = claims.FirstOrDefault(x => x.Type == "Photo");
            if (photoClaim is not null)
            {
                photoUser = photoClaim.Value;
            }
        }

        private void ShowModal()
        {
            Modal.Show<Login>();
        }
    }
}