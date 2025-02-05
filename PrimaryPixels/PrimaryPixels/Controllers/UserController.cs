using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PrimaryPixels.DTO;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixels.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;
    public UserController(IUserRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<UserResponse>> GetUserInfos()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("UserId not found.");
        }
        var userInfos = await _repository.GetUserById(userId);
        return Ok(userInfos);
    }

    [HttpPatch]
    public async Task<IActionResult> ChangeUserPassword(string newPassword)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("UserId not found.");
        }
        var success = await _repository.ChangePasswordAsync(newPassword, userId);
        if (!success)
        {
            return BadRequest("Failed to change password.");
        }
        return Ok("Password changed successfully.");
    }
}