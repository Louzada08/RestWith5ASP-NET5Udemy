using RestWithASPNET.Domain.Entities;

namespace RestWithASPNET.Domain.Dtos;

public class UserDTO
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiryTime { get; set; }
    public UserRolesEnum UserRole { get; set; }
}
