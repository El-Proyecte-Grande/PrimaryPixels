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
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            throw new InvalidOperationException("Couldn't find user with this id");
        }

        return new UserResponse(user.UserName, user.Email);
    }

    public async Task<bool> ChangePasswordAsync(string newPassword, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return false;
        }
        var result = await _userManager.ChangePasswordAsync(user, user.PasswordHash, newPassword);
        return result.Succeeded;
    }
    
}