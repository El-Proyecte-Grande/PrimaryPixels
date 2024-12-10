using Microsoft.AspNetCore.Identity;

namespace PrimaryPixels.Services.Authentication;

public interface ITokenService
{
    public string CreateToken(IdentityUser user, string role);
}