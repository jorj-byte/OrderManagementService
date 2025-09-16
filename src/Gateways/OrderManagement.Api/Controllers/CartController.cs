
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Commands;
using OrderApplication.Queries;
using Shared.Identity;

namespace OrderManagement.Controllers;

[ApiController]
[Route("cart")]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator) => _mediator = mediator;
    

    // POST /cart/{orderId}
    [HttpPost("{orderId}")]
    public async Task<IActionResult> GetCart(Guid orderId)
    {
        var userId = this.GetUserId();
        var result = await _mediator.Send(new GetCartQuery(orderId, userId));
        return Ok(result);
    }

    // POST /cart/items
    [HttpPost("items")]
    public async Task<IActionResult> AddItem([FromBody] AddCartItemCommand command)
    {
        var updatedCommand = command with { UserId = this.GetUserId() };
        var result = await _mediator.Send(updatedCommand);
        return Ok(result);
    }

    // DELETE /cart/items/{itemId}
    [HttpDelete("items/{itemId}")]
    public async Task<IActionResult> RemoveItem(Guid itemId)
    {
        var userId =this.GetUserId();
        await _mediator.Send(new RemoveCartItemCommand(itemId, userId));
        return NoContent();
    }
}