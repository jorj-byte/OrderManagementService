using Financial.Application.Queries;
using Financial.Domain.Entities;
using MediatR;
using Shared.Kernel;

namespace Financial.Application.Handlers;

public class GetPaymentsQueryHandler:IRequestHandler<GetPaymentsQuery,IReadOnlyList<PaymentDto>>
{
    private IRepository<Payment> _payments;

    public GetPaymentsQueryHandler(IRepository<Payment> payments)
    {
        _payments = payments;
    }

    public async Task<IReadOnlyList<PaymentDto>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
    {
        var result= _payments.Query().Where(d=>d.UserId==request.UserId)
            .Select(c=>new PaymentDto(c.OrderId,c.UserId,c.Amount,c.PaymentDate));
        return result.ToList();
    }
}