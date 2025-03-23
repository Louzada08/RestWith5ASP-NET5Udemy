using MediatR;

namespace RestWithASPNET.Application.Commands.Users.Requests;

public class FindUserByIdRequest : IRequest<FindUserByIdResponse>
{
    public Guid Id { get; set; }
}