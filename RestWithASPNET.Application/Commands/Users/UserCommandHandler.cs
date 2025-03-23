using AutoMapper;
using MediatR;
using RestWithASPNET.Application.Commands.Users.Requests;
using RestWithASPNET.Application.Interfaces.Users;
using RestWithASPNET.Domain.Entities;
using RestWithASPNET.Domain.Interfaces.Repositories;
using RestWithASPNET.Domain.Messages;
using RestWithASPNET.Domain.Validation;

namespace RestWithASPNET.Application.Commands.Users;

public class UserCommandHandler : CommandHandler,
    IRequestHandler<CreateUserRequest, ValidationResultBag>,
    IRequestHandler<UpdateUserRequest, ValidationResultBag>,
    IRequestHandler<PatchUserRequest, ValidationResultBag>,
    IRequestHandler<DeleteUserRequest, ValidationResultBag>,
    IRequestHandler<FindUserByIdRequest, FindUserByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;

    public UserCommandHandler(IMapper mapper, IUserRepository userRepository, IUserService userService)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _userService = userService;
    }

    public async Task<ValidationResultBag> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);

        var response = user.CanAdd(_userRepository, user);

        if (!response.IsValid)
        {
            ValidationResult.Data = response;
            return ValidationResult;
        }

        var ret = await _userService.Create(user);

        ValidationResult.Data = _mapper.Map<CreateUserResponse>(ret);

        return ValidationResult;
    }

    public async Task<ValidationResultBag> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);

        var ret = _userRepository.Update(user);
        await PersistData(_userRepository.UnitOfWork);

        ValidationResult.Data = _mapper.Map<UpdateUserResponse>(ret);

        return ValidationResult;
    }

    public async Task<ValidationResultBag> Handle(PatchUserRequest request, CancellationToken cancellationToken)
    {
        var user = _userService.GetById(request.Id);

        var patchUser = _mapper.Map<PatchUserRequest>(user);

        request.PatchUser.ApplyTo(patchUser);

        _mapper.Map(patchUser, user);

        var ret = _userRepository.Update(user);
        await PersistData(_userRepository.UnitOfWork);

        ValidationResult.Data = _mapper.Map<PatchUserResponse>(ret);

        return ValidationResult;
    }

    public async Task<ValidationResultBag> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var user = _userRepository.FindById(request.Id);

        if (!request.IsValid()) return ValidationResult;

        if (user == null)
        {
            AddError("Usuário não existe.");
            return ValidationResult;
        }

        _userRepository.Delete(user);
        await PersistData(_userRepository.UnitOfWork);

        return ValidationResult;
    }

    public Task<FindUserByIdResponse> Handle(FindUserByIdRequest request, CancellationToken cancellationToken)
    {
        var user = _userRepository.FindById(request.Id);

        var result = _mapper.Map<FindUserByIdResponse>(user);

        return Task.FromResult(result);
    }
}
