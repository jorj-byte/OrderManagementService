using MediatR;
using OrderApplication.Queries;
using Shared.Kernel;

namespace Order.Application.Handlers;

public class GetCartQueryHandler:IRequestHandler<GetCartQuery,CartDto>
{
   private  readonly  IRepository<Domain.Entities.Order> _orders;

   public GetCartQueryHandler(IRepository<Domain.Entities.Order> orders)
   {
       _orders = orders;
   }

   public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var order = await _orders.GetAsync(request.OrderId,cancellationToken);

        return new CartDto(order.Id, order.UserId, order.Items.
            Select(i => new CartItemDto(ItemId: i.Id, ProductName: i.ProductName, Amount: i.Amount)).ToList());
    }
}