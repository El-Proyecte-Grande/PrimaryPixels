using System.ComponentModel.DataAnnotations;

namespace PrimaryPixels.Contracts;

public record AuthRequest(
    [Required] string Email,
    [Required] string Password
    );