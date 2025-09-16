namespace OrderApplication.Queries;

public record CartDto(Guid OrderId, Guid UserId, IReadOnlyList<CartItemDto> Items);