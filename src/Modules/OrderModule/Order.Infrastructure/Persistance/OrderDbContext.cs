using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Shared.Kernel;

namespace Order.Infrastructure.Persistance;

public class OrderDbContext : DbContext, IUnitOfWork
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) :
        base(options)
    {
    }

    public DbSet<Domain.Entities.Order> Orders => Set<Domain.Entities.Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public Task<int> SaveChangesAsync(CancellationToken ct = default) =>
        base.SaveChangesAsync(ct);
    public void Dispose() => base.Dispose();
}