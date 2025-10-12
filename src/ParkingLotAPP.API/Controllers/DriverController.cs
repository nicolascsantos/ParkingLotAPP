using Microsoft.AspNetCore.Mvc;
using MediatR;
using ParkingLotAPP.Application.UseCases.Driver.CreateDriver;
using ParkingLotAPP.API.APIModels;
using ParkingLotAPP.Application.UseCases.Driver.Common;

namespace ParkingLotAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DriverController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDriverInput input, CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(input);
            return CreatedAtAction(nameof(Create), new { output.Id }, new APIResponse<DriverModelOutput>(output));
        }
    }
}
