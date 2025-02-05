using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrimaryPixels.Data;
using PrimaryPixels.DTO;

namespace PrimaryPixels.Services.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<IdentityUser> _userManager;
    public UserRepository(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<UserResponse> GetUserById(string id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        return new UserResponse() { Email = user.Email, Username = user.UserName };
    }

    public async Task<bool> ChangePasswordAsync(string newPassword, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return false;
        }

        var result = await _userManager.ChangePasswordAsync(user, user.PasswordHash, newPassword);
        if (result.Succeeded)return true;
        return false;
    }
    
}