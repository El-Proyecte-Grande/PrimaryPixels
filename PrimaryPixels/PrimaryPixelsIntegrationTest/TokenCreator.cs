using Microsoft.AspNetCore.Identity;
using PrimaryPixels.Services.Authentication;

namespace PrimaryPixelsIntegrationTest;

public class TokenCreator
{
    public ITokenService TokenService;
    public TokenCreator(ITokenService tokenService)
    {
        TokenService = tokenService;
    }
    public string GenerateJwtToken(string role = "User")
    {
        var user = new IdentityUser(){UserName = "TestUser", Email = "TestEmail"};
        return TokenService.CreateToken(user, role);
    }
}