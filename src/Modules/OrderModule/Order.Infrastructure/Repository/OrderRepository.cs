
using Microsoft.EntityFrameworkCore;
using Order.Domain;
using Order.Infrastructure.Persistance;
using OrderApplication.Contracts;
using Shared.Infrastructure;
using Shared.Kernel;
using Entities= Order.Domain.Entities;
namespace Order.Infrastructure.Repository;

internal class OrderRepository : DbRepository<Entities.Order>, IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Entities.Order?> GetByIdWithItemsAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
    }

    public async Task<Entities.Order?> GetPendingOrderForUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.UserId == userId && o.Status == OrderStatus.Pending, cancellationToken);
    }
}