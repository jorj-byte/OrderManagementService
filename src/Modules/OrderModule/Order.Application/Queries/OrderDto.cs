using Order.Domain;

namespace OrderApplication.Queries;

public record OrderDto(Guid Id,OrderStatus Status,decimal Amount,DateTime CreatedAt);