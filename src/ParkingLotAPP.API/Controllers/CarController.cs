using ParkingLotAPP.API.APIModels;
using ParkingLotAPP.API.APIModels.Response;
using ParkingLotAPP.Application.UseCases.Car.Common;
using ParkingLotAPP.Application.UseCases.Car.CreateCar;
using ParkingLotAPP.Application.UseCases.Car.DeleteCar;
using ParkingLotAPP.Application.UseCases.Car.GetCar;
using ParkingLotAPP.Application.UseCases.Car.ListCars;
using ParkingLotAPP.Application.UseCases.Car.UpdateCar;
using ParkingLotAPP.Domain.SeedWork.SearchableRepository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ParkingLotAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(201, StatusCode = StatusCodes.Status201Created, Type = typeof(APIResponse<CarModelOutput>))]
        [ProducesResponseType(422, StatusCode = StatusCodes.Status422UnprocessableEntity, Type = typeof(APIResponse<CarModelOutput>))]
        [ProducesResponseType(400, StatusCode = StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Post([FromBody] CreateCarInput input, CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(input);
            return CreatedAtAction(nameof(Post), new { output.Id }, new APIResponse<CarModelOutput>(output));
        }

        [HttpDelete("{id::guid}")]
        [ProducesResponseType(204, StatusCode = StatusCodes.Status204NoContent, Type = typeof(object))]
        [ProducesResponseType(404, StatusCode = StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(new DeleteCarInput(id));
            return NoContent();
        }

        [HttpGet("{id::guid}")]
        [ProducesResponseType(404, StatusCode = StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(200, StatusCode = StatusCodes.Status200OK, Type = typeof(APIResponse<CarModelOutput>))]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(new GetCarInput(id), cancellationToken);
            return Ok(new APIResponse<CarModelOutput>(output));
        }

        [HttpGet]
        public async Task<IActionResult> Search(
            CancellationToken cancellationToken,
            [FromQuery] int? page = null,
            [FromQuery(Name = "per_page")] int? perPage = null,
            [FromQuery] string? search = null,
            [FromQuery] string? sort = null,
            [FromQuery] SearchOrder? dir = null
        )
        {
            var input = new ListCarsInput();

            if (page is not null) input.Page = page.Value;
            if (perPage is not null) input.PerPage = perPage.Value;
            if (!string.IsNullOrWhiteSpace(search)) input.Search = search;
            if (!string.IsNullOrWhiteSpace(sort)) input.Sort = sort;
            if (dir is not null) input.Dir = dir.Value;

            var output = await _mediator.Send(input, cancellationToken);

            return Ok(new APIResponseList<CarModelOutput>(output));
        }

        [HttpPut]
        [ProducesResponseType(200, StatusCode = StatusCodes.Status200OK, Type = typeof(APIResponse<CarModelOutput>))]
        [ProducesResponseType(400, StatusCode = StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(404, StatusCode = StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Put([FromBody] UpdateCarInput input, CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(input, cancellationToken);

            return Ok(new APIResponse<CarModelOutput>(output));
        }
    }
}
