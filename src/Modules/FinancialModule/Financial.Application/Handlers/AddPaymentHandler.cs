using Financial.Application.Commands;
using Financial.Domain.Entities;
using MediatR;
using Shared.Kernel;

namespace Financial.Application.Handlers;

public class AddPaymentHandler:IRequestHandler<AddPaymentCommand,Guid>
{
    private IRepository<Payment> _payments;
    private readonly IUnitOfWork _uow;

    public AddPaymentHandler(IRepository<Payment> payments, IUnitOfWork uow)
    {
        _payments = payments;
        _uow = uow;
    }


    public async Task<Guid> Handle(AddPaymentCommand request, CancellationToken ct)
    {
        var payment = new Payment() {Amount = request.Amount, OrderId = request.OrderId, UserId = request.UserId};
        await _payments.AddAsync(payment,ct);
        await _uow.SaveChangesAsync(ct);
        return payment.Id;
    }
}