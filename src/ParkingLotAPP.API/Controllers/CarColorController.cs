using ParkingLotAPP.API.APIModels;
using ParkingLotAPP.API.APIModels.Response;
using ParkingLotAPP.Application.UseCases.CarColor.Common;
using ParkingLotAPP.Application.UseCases.CarColor.CreateCarColor;
using ParkingLotAPP.Application.UseCases.CarColor.DeleteCarColor;
using ParkingLotAPP.Application.UseCases.CarColor.ListCarColors;
using ParkingLotAPP.Application.UseCases.CarColor.UpdateCarColor;
using ParkingLotAPP.Domain.SeedWork.SearchableRepository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ParkingLotAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarColorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarColorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCarColorInput input, CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(input);
            return CreatedAtAction(nameof(Create), new { output.Id }, new APIResponse<CarColorModelOutput>(output));
        }

        [HttpGet]
        public async Task<IActionResult> Search(
            CancellationToken cancellationToken,
            [FromQuery] int? page = null,
            [FromQuery(Name = "per_page")] int? perPage = null,
            [FromQuery] string? search = null,
            [FromQuery] string? sort = null,
            [FromQuery] SearchOrder? dir = null)
        {
            var input = new ListCarColorsInput();

            if (page is not null) input.Page = page.Value;
            if (perPage is not null) input.PerPage = perPage.Value;
            if (!string.IsNullOrWhiteSpace(search)) input.Search = search;
            if (!string.IsNullOrWhiteSpace(sort)) input.Sort = sort;
            if (dir is not null) input.Dir = dir.Value;

            var output = await _mediator.Send(input, cancellationToken);

            return Ok(new APIResponseList<CarColorModelOutput>(output));
        }

        [HttpDelete("{id::guid}")]
        [ProducesResponseType(204, StatusCode = StatusCodes.Status204NoContent, Type = typeof(object))]
        [ProducesResponseType(404, StatusCode = StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(new DeleteCarColorInput(id), cancellationToken);
            return NoContent();
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCarColorInput input, CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(input, cancellationToken);
            return Ok(new APIResponse<CarColorModelOutput>(output));
        }
    }
}
