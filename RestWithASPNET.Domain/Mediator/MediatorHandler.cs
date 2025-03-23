using MediatR;
using RestWithASPNET.Domain.Mediator.Interfaces;
using RestWithASPNET.Domain.Messages;
using RestWithASPNET.Domain.Validation;

namespace RestWithASPNET.Domain.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T evnt) where T : Event
        {
            await _mediator.Publish(evnt);
        }

        public async Task<ValidationResultBag> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }
    }
}
