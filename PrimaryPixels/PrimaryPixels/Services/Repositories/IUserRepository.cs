using PrimaryPixels.DTO;

namespace PrimaryPixels.Services.Repositories;

public interface IUserRepository
{
    public Task<UserResponse> GetUserById(string id);
    public Task<bool> ChangePasswordAsync(string newPassword, string userId);
}