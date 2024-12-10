namespace PrimaryPixels.Services.Authentication;

public record AuthResult(
    bool Success,
    string Username,
    string Email,
    string Token
)
{
    public readonly Dictionary<string, string> ErrorMessages = new();
}

