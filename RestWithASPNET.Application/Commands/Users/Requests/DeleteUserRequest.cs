using RestWithASPNET.Domain.Messages;

namespace RestWithASPNET.Application.Commands.Users.Requests;

public class DeleteUserRequest : Command
{
    public DeleteUserRequest(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private set; }

    public override bool IsValid()
    {
        return Guid.Empty != Id;
    }
}