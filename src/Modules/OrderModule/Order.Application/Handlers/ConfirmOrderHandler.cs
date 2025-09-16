using MassTransit;
using MediatR;
using Order.Application.Commands;
using Order.Domain;
using Shared.Infrastructure.MessageContracts;
using Shared.Kernel;

namespace Order.Application.Handlers;

public class ConfirmOrderHandler:IRequestHandler<ConfirmOrderCommand>
{
    private readonly IRepository<Domain.Entities.Order> _orders;
    private readonly IUnitOfWork _uow;
    private readonly IPublishEndpoint _publish; // MassTransit
    public ConfirmOrderHandler(IRepository<Domain.Entities.Order> orders, IUnitOfWork uow,
        IPublishEndpoint publish) {
        _orders = orders; _uow = uow; _publish = publish;
    }
    public async Task Handle(ConfirmOrderCommand req, CancellationToken ct)
    {
        var order = await _orders.GetAsync(req.OrderId, ct);
        var amount = order.Items.Sum(i => i.Amount);
        if (order.UserId != req.UserId) throw new UnauthorizedAccessException();
        order.Status = OrderStatus.Confirmed;
        await _uow.SaveChangesAsync(ct);
        await _publish.Publish(new OrderConfirmedEvent(order.Id, order.UserId,amount),
            ct); 
    }
}