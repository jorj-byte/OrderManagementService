namespace OrderApplication.Queries;

public record CartItemDto(Guid ItemId, string ProductName, int Amount);