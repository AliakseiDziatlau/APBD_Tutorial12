using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tutorial12.Application.Commands;
using Tutorial12.Application.Queries;

namespace Tutorial12.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TripsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllTripsQuery { Page = page, PageSize = pageSize });
        return Ok(result);
    }

    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] AssignClientToTripCommand command)
    {
        command.IdTrip = idTrip;
        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(
            _ => Ok(new { message = "Client successfully registered." }),
            _ => Conflict(new { message = "Client with this PESEL already exists." }),
            _ => Conflict(new { message = "Client already registered for this trip." }),
            _ => NotFound(new { message = "Trip not found or already started." }),
            _ => BadRequest(new { message = "Trip name does not match the given trip ID." }) 
        );
    }
}