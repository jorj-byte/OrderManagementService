using Shared.Kernel;
using Entities=Order.Domain.Entities;

namespace OrderApplication.Contracts;

public interface IOrderRepository : IRepository<Entities.Order>
{
    Task<Entities.Order?> GetByIdWithItemsAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<Entities.Order?> GetPendingOrderForUserAsync(Guid userId, CancellationToken cancellationToken = default);
}