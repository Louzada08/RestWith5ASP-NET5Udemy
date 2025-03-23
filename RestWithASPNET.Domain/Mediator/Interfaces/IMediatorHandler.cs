using RestWithASPNET.Domain.Messages;
using RestWithASPNET.Domain.Validation;

namespace RestWithASPNET.Domain.Mediator.Interfaces;

public interface IMediatorHandler
{
    public Task PublishEvent<T>(T evnt) where T : Event;
    public Task<ValidationResultBag> SendCommand<T>(T command) where T : Command;
}
