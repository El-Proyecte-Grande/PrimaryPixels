using System.ComponentModel.DataAnnotations;

namespace PrimaryPixels.Contracts;

public record RegistrationRequest(
    [Required] string Email,
    [Required] string Username,
    [Required] string Password
    );