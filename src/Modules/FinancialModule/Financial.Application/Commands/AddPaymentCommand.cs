using MediatR;

namespace Financial.Application.Commands;

public record AddPaymentCommand( Guid OrderId,Guid UserId,decimal Amount): IRequest<Guid>;