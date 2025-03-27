using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoring.Application.Commands;
using TheatricalPlayersRefactoring.Application.Queries;

namespace TheatricalPlayersRefactoring.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillController : ControllerBase
{
    private readonly IMediator _mediator;

    public BillController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<GenerateBillResult>> Generate([FromBody] GenerateBillCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var query = new GetBillQuery(id);
        var result = await _mediator.Send(query);
        
        return File(System.Text.Encoding.UTF8.GetBytes(result.Content), result.ContentType, $"bill.{result.Format.ToLower()}");
    }
}
