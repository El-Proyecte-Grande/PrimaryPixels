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

    public async Task<bool> ChangePasswordAsync(string currentPassword, string newPassword, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return false;
        }
        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        return result.Succeeded;
    }

    public async Task<string> GetPasswordResetToken(string email)
    {
        IdentityUser? user = await _userManager.FindByEmailAsync(email);
        if(user == null) throw new InvalidOperationException("Couldn't find user with this email");
        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<bool> ResetPassword(string email, string token, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            throw new InvalidOperationException("Couldn't find user with this email");
        }
        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        return result.Succeeded;
    }
}