namespace PrimaryPixels.Contracts;

public record AuthResponse(
    string Email,
    string Username,
    string Token
    );