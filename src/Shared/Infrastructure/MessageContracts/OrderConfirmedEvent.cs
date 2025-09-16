namespace Shared.Infrastructure.MessageContracts;

public record OrderConfirmedEvent(Guid OrderId,Guid UserId,decimal Payment);