using PrimaryPixels.DTO;

namespace PrimaryPixels.Services.Repositories;

public interface IUserRepository
{
    public Task<UserResponse> GetUserById(string id);
    public Task<bool> ChangePasswordAsync(string currentPassword, string newPassword, string userId);
}