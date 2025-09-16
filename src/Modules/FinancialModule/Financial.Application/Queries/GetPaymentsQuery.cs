using MediatR;

namespace Financial.Application.Queries;

public record GetPaymentsQuery(Guid UserId) : IRequest<IReadOnlyList<PaymentDto>>;
