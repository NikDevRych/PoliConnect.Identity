namespace PoliConnect.Identity.Application.DTOs;

public sealed class RegisterDTO
{
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Password { get; set; }
}
