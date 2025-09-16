using Shared.Entities;

namespace Order.Domain.Entities;

public class OrderItem:BaseEntity
{
    public Guid OrderId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Amount { get; set; }
}