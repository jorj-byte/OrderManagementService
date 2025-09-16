using Order.Application.Commands;
using MediatR;
using Order.Domain.Entities;
using Entities = Order.Domain.Entities;
using Shared.Kernel;

namespace Order.Application.Handlers;

internal class RemoveCartItemHandler : IRequestHandler<RemoveCartItemCommand>
{
    private readonly IRepository<OrderItem> _items;
    private readonly IRepository<Domain.Entities.Order> _orders;
    private readonly IUnitOfWork _uow;
    public RemoveCartItemHandler(IRepository<OrderItem> items,
        IRepository<Entities.Order> orders, IUnitOfWork uow) { _items = items; _orders = orders;
        _uow = uow; }
    public async Task Handle(RemoveCartItemCommand req, CancellationToken
        ct)
    {
        var item = _items.Query().FirstOrDefault(i => i.Id == req.ItemId);
        if (item == null) throw new KeyNotFoundException("Item not found");
        var order = await _orders.GetAsync(item.OrderId, ct);
        if (order == null || order.UserId != req.UserId) throw new
            UnauthorizedAccessException();
        _items.Remove(item);
        await _uow.SaveChangesAsync(ct);
    }
}
