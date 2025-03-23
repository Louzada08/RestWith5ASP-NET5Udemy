using RestWithASPNET.Domain.Messages;

namespace RestWithASPNET.Application.Commands.Users.Requests;

public class UpdateUserRequest : Command
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
