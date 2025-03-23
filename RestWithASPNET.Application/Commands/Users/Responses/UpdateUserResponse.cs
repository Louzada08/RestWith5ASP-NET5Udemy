
using RestWithASPNET.Domain.Entities;

namespace RestWithASPNET.Application.Commands.Users.Requests;

public class UpdateUserResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRolesEnum AppUserRole { get; set; }
}