using Financial.Domain.Entities;
using MassTransit;
using Shared.Infrastructure.MessageContracts;
using Shared.Kernel;

namespace Financial.Application.Consumers;

public class OrderConfirmedConsumer:IConsumer<OrderConfirmedEvent>
{
    private readonly IRepository<Payment> _payments;
    private IUnitOfWork _unitOfWork;
    public OrderConfirmedConsumer(IRepository<Payment> payments, IUnitOfWork unitOfWork)
    {
        _payments = payments;
        _unitOfWork = unitOfWork;
    }

    public async Task Consume(ConsumeContext<OrderConfirmedEvent> context)
    {
        var order = context.Message;
        var payment = new Payment() {OrderId = order.OrderId,UserId = order.UserId,Amount = order.Payment};
        await _payments.AddAsync(payment);
        await _unitOfWork.SaveChangesAsync();
    }
}