using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using PrimaryPixels.DTO;
using PrimaryPixels.Exceptions;
using PrimaryPixels.Services.Authentication;
using PrimaryPixels.Services.Repositories;
using ForgotPasswordRequest = PrimaryPixels.DTO.ForgotPasswordRequest;
using ResetPasswordRequest = PrimaryPixels.DTO.ResetPasswordRequest;

namespace PrimaryPixels.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;
    private readonly IEmailSender _emailSender;
    private readonly IConfiguration _configuration;
    public UserController(IUserRepository repository, IEmailSender emailSender, IConfiguration configuration)
    {
        _repository = repository;
        _emailSender = emailSender;
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<ActionResult<UserResponse>> GetUserInfos()
    {
        try
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("UserId not found.");
            }
            var userInfos = await _repository.GetUserById(userId);
        
            return Ok(userInfos);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPatch]
    public async Task<IActionResult> ChangeUserPassword([FromBody] ChangePasswordRequest model)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("UserId not found.");
        }
         var success = await _repository.ChangePasswordAsync(model.CurrentPassword, model.NewPassword, userId);
        if (!success)
        {
            return BadRequest("Failed to change password.");
        }
        return Ok("Password changed successfully.");
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return BadRequest("Email cannot be empty");
            }

            var token = await _repository.GetPasswordResetToken(request.Email);
            string resetLink = $"{_configuration["FrontendUrl"]}/reset/password/new?email={Uri.EscapeDataString(request.Email)}&token={token}";
            await _emailSender.SendEmailAsync(
                request.Email,
                "Password Reset",
                $"Please reset your password by clicking <a href='{resetLink}'>here</a>.");
            return Ok("Password reset link sent to your email.");
        }
        catch (EmailNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred: " + ex.Message);
        }
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Token) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.NewPassword))
            {
                return BadRequest("Invalid request.");
            }
            var succeed = await _repository.ResetPassword(request.Email, request.Token, request.NewPassword);
            if(!succeed) return BadRequest("Error occured while resetting password.");
            return Ok("Password reset successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}