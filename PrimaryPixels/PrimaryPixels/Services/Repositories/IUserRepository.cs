using Microsoft.AspNetCore.Identity;
using PrimaryPixels.DTO;

namespace PrimaryPixels.Services.Repositories;

public interface IUserRepository
{
    public Task<UserResponse> GetUserById(string id);
    public Task<bool> ChangePasswordAsync(string currentPassword, string newPassword, string userId);
    public Task<string> GetPasswordResetToken(string email);
    public Task<bool> ResetPassword(string email, string token, string newPassword);
}