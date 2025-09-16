using Shared.Entities;

namespace Order.Domain.Entities;

public class Order:BaseEntity
{
    public Guid UserId { get; set; }
    public OrderStatus Status { get; set; } =OrderStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<OrderItem> Items { get; set; } = new();
}