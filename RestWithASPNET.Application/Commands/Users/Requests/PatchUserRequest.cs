using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using RestWithASPNET.Domain.Interfaces.Base;
using RestWithASPNET.Domain.Messages;

namespace RestWithASPNET.Application.Commands.Users.Requests;

public class PatchUserRequest : Command, IBase
{
    public PatchUserRequest(JsonPatchDocument<PatchUserRequest> patchUser)
    {
        PatchUser = patchUser;
    }

    public PatchUserRequest()
    { }

    public JsonPatchDocument<PatchUserRequest> PatchUser { get; }
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public override bool IsValid()
    {
        ValidationResult = new PatchUserValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class PatchUserValidation : AbstractValidator<PatchUserRequest>
{
    // R.C -> Example Validation
    public PatchUserValidation()
    {
        //RuleFor(c => c.Id)
        //    .NotEqual(Guid.Empty)
        //    .WithMessage("Id inválido");

        //RuleFor(c => c.Cpf)
        //    .Must(TerCpfValido)
        //    .WithMessage("O CPF informado não é valido");

        //RuleFor(c => c.Email)
        //    .Must(TerEmailValido)
        //    .WithMessage("O e-mail informado não é valido");
    }

    //protected static bool TerCpfValido(string cpf)
    //{
    //    return ValidateCPF.IsValidCpf(cpf);
    //}

    //protected static bool TerEmailValido(string email)
    //{
    //    return ValidateEmail.IsValidEmail(email);
    //}
}