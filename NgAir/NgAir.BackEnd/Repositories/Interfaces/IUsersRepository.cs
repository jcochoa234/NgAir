using Microsoft.AspNetCore.Identity;
using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;

namespace NgAir.BackEnd.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<IdentityResult> AddUserAsync(User user, string password);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);

        Task CheckRoleAsync(string roleName);

        Task<IdentityResult> ConfirmEmailAsync(User user, string token);

        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<string> GeneratePasswordResetTokenAsync(User user);

        Task<User> GetUserAsync(Guid userId);

        Task<User> GetUserAsync(string email);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> LoginAsync(LoginDto model);

        Task LogoutAsync();

        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);

        Task<IdentityResult> UpdateUserAsync(User user);

    }
}
