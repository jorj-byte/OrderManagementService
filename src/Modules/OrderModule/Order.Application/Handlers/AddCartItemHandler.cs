using Order.Application.Commands;
using MediatR;
using Order.Domain.Entities;
using Order.Domain;
using Shared.Kernel;
using Entities = Order.Domain.Entities;

namespace Order.Application.Handlers;

public class AddCartItemHandler : IRequestHandler<AddCartItemCommand, Guid>
{
    private readonly IRepository<Domain.Entities.Order> _orders;
    private readonly IRepository<OrderItem> _items;
    private readonly IUnitOfWork _uow;
    public AddCartItemHandler(IRepository<Domain.Entities.Order> orders, IRepository<OrderItem>
        items, IUnitOfWork uow) {
        _orders = orders; _items = items; _uow = uow;
    }
    public async Task<Guid> Handle(AddCartItemCommand req, CancellationToken ct)
    {
        
        var order = _orders.Query().FirstOrDefault(o => o.UserId == req.UserId
                                                        && o.Status == OrderStatus.Pending);
        
        if (order == null)
        {
            order = new Entities.Order { UserId = req.UserId, Status =
                OrderStatus.Pending };
            await _orders.AddAsync(order, ct);
            await _uow.SaveChangesAsync(ct);
        }
        var item = new OrderItem { OrderId = order.Id, ProductName =
            req.ProductName, Amount = req.Amount };
        await _items.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return order.Id;
    }
}
