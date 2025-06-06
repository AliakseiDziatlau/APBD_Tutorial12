using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tutorial12.Application.Commands;

namespace Tutorial12.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClientsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient([FromQuery] int id)
    {
        var result = await _mediator.Send(new DeleteClientCommand { Id = id });

        return result.Match<IActionResult>(
            _ => NoContent(),  
            _ => NotFound(new { message = "Client not found." }),
            _ => Conflict(new { message = "Client has assigned trips and cannot be deleted." })
        );
    }
}