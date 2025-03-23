using FluentValidation.Results;
using MediatR;
using RestWithASPNET.Domain.Validation;

namespace RestWithASPNET.Domain.Messages;
public abstract class Command : IRequest<ValidationResultBag>
{
    public DateTime Timestamp { get; private set; }
    public ValidationResult ValidationResult { get; set; }

    protected Command() => Timestamp = DateTime.Now;

    public virtual bool IsValid()
    {
        throw new NotImplementedException();
    }
}
