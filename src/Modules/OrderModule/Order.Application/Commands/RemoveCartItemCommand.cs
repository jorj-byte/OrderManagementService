using MediatR;

namespace Order.Application.Commands;

public record RemoveCartItemCommand(Guid UserId, Guid ItemId) : IRequest;