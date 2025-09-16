using Financial.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Identity;

namespace OrderManagement.Controllers;

[ApiController]
[Route("payments")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator) => _mediator = mediator;

 
    // GET /payments
    [HttpGet]
    public async Task<IActionResult> GetPayments()
    {
        var userId = this.GetUserId();
        var result = await _mediator.Send(new GetPaymentsQuery(userId));
        return Ok(result);
    }
}