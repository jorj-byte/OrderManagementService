using MediatR;

namespace OrderApplication.Queries;

public record GetCartQuery(Guid OrderId, Guid UserId):IRequest<CartDto>;