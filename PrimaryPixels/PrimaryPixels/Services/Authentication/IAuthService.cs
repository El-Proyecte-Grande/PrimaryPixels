namespace PrimaryPixels.Services.Authentication;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(string username, string email, string password, string role);
    Task<AuthResult> LoginAsync(string email, string password);
}