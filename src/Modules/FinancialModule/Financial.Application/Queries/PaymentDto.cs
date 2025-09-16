namespace Financial.Application.Queries;

public record PaymentDto(Guid OrderId, Guid UserId, decimal Amount, DateTime PaymentDate);