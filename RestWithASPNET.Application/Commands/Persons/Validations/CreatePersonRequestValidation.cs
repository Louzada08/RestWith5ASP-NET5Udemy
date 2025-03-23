using FluentValidation;
using RestWithASPNET.Application.Commands.Persons.Requests;

namespace RestWithASPNET.Application.Commands.Persons.Validations;

public class CreatePersonRequestValidation : AbstractValidator<CreatePersonRequest>
{
    public CreatePersonRequestValidation()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("O campo Nome é obrigatório");
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("O campo Sobrenome é obrigatório");
        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("O campo Endereço é obrigatório");
    }
}
