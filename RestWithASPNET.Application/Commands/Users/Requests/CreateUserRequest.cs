using RestWithASPNET.Domain.Entities;
using RestWithASPNET.Domain.Messages;

namespace RestWithASPNET.Application.Commands.Users.Requests;

public class CreateUserRequest : Command
{
    public string UserName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiryTime { get; set; }
    public UserRolesEnum UserRole { get; set; }
}
