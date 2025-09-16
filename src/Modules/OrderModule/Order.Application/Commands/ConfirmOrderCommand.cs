using MediatR;

namespace Order.Application.Commands;

public record ConfirmOrderCommand(Guid UserId, Guid OrderId) : IRequest;