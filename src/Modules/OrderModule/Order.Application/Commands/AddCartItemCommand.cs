using MediatR;

namespace Order.Application.Commands;

public record AddCartItemCommand(Guid UserId, string ProductName, int Amount) :
    IRequest<Guid>;
