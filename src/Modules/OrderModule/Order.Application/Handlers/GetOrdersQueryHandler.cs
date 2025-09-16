using System.Collections.Immutable;
using MediatR;
using OrderApplication.Contracts;
using OrderApplication.Queries;
using Shared.Kernel;

namespace Order.Application.Handlers;

public class GetOrdersQueryHandler:IRequestHandler<GetOrdersQuery,IReadOnlyList<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICacheService _cacheService;

    public GetOrdersQueryHandler(IOrderRepository orderRepository, ICacheService cacheService)
    {
        _orderRepository = orderRepository;
        _cacheService = cacheService;
    }

    public async Task<IReadOnlyList<OrderDto>> Handle(GetOrdersQuery request, CancellationToken ct)
    {
        var cacheKey = $"orders:{request.UserId}";

        return await _cacheService.GetOrAddAsync(cacheKey, async () =>
        {
            return _orderRepository.Query().Where(c => c.UserId == request.UserId).Select(c =>
                new OrderDto(c.Id, c.Status, c.Items.Sum(d => d.Amount), c.CreatedAt)).ToImmutableList();
        }, TimeSpan.FromMinutes(5), ct);
       
    }
}