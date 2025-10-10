using MassTransit;
using MediatR;
using ProfileCore.Application.Query;

namespace ProfileCore.Application.Consumers
{
    public class OrderCreateCommandConsumer : IConsumer<OrderCreateCommand>
    {
        private readonly IMediator _mediator;

        public OrderCreateCommandConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<OrderCreateCommand> context)
        {
            var result = await _mediator.Send(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}