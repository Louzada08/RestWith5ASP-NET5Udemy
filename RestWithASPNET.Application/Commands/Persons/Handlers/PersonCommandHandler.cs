using AutoMapper;
using MediatR;
using RestWithASPNET.Application.Commands.Persons.Requests;
using RestWithASPNET.Application.Commands.Persons.Responses;
using RestWithASPNET.Application.Services.Persons;
using RestWithASPNET.Domain.Entities;
using RestWithASPNET.Domain.Interfaces.Repositories;
using RestWithASPNET.Domain.Messages;
using RestWithASPNET.Domain.Validation;

namespace RestWithASPNET.Application.Commands.Persons.Handlers;

public class PersonCommandHandler : CommandHandler,
    IRequestHandler<CreatePersonRequest, ValidationResultBag>
{
    private readonly IPersonService _personService;
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public PersonCommandHandler(IPersonService personService, IPersonRepository personRepository, IMapper mapper)
    {
        _personService = personService;
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<ValidationResultBag> Handle(CreatePersonRequest request, CancellationToken cancellationToken)
    {
        if (!request.IsValid())
        {
            ValidationResult.Errors.AddRange(request.ValidationResult.Errors);
            return ValidationResult;
        }

        var personMap = _mapper.Map<Person>(request);

        var ret = await _personService.Create(personMap);

        ValidationResult.Data = _mapper.Map<CreatePersonResponse>(ret);

        return ValidationResult;
    }
}
