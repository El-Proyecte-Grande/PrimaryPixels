namespace PrimaryPixels.DTO;

public class ResetPasswordRequest
{
    public string Token { get; init; }
    public string Email { get; init; }
    public string NewPassword { get; init; }
}