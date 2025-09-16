using Shared.Entities;

namespace Financial.Domain.Entities;

public class Payment : BaseEntity
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
}