using System.Threading.Tasks;
using NgAir.FrontEnd.Models;
using NgAir.FrontEnd.Services;
using Microsoft.AspNetCore.Components;

namespace NgAir.FrontEnd.Pages.Account.Settings
{
    public partial class BaseView
    {
        private CurrentUser _currentUser = new CurrentUser();

        [Inject] protected IUserService UserService { get; set; }

        private void HandleFinish()
        {
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _currentUser = await UserService.GetCurrentUserAsync();
        }
    }
}