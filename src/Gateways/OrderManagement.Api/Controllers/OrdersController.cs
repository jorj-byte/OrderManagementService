using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Commands;
using OrderApplication.Queries;
using Shared.Identity;

namespace OrderManagement.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator) => _mediator = mediator;



    // POST /orders/confirm
    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmOrder([FromBody] ConfirmOrderCommand command)
    {
        var updatedCommand = command with { UserId = this.GetUserId() };
        await _mediator.Send(updatedCommand);
        return Ok("Order confirmed");
    }

    // GET /orders
    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var userId = this.GetUserId();
        var result = await _mediator.Send(new GetOrdersQuery(userId));
        return Ok(result);
    }
}