using MediatR;

namespace OrderApplication.Queries;

public record GetOrdersQuery(Guid UserId):IRequest<IReadOnlyList<OrderDto>>;