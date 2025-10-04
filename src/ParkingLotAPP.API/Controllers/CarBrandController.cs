using ParkingLotAPP.API.APIModels;
using ParkingLotAPP.API.APIModels.CarBrand;
using ParkingLotAPP.API.APIModels.Response;
using ParkingLotAPP.Application.UseCases.CarBrand.Common;
using ParkingLotAPP.Application.UseCases.CarBrand.CreateCarBrand;
using ParkingLotAPP.Application.UseCases.CarBrand.DeleteCarBrand;
using ParkingLotAPP.Application.UseCases.CarBrand.GetCarBrand;
using ParkingLotAPP.Application.UseCases.CarBrand.ListCarBrands;
using ParkingLotAPP.Application.UseCases.CarBrand.UpdateCarBrand;
using ParkingLotAPP.Domain.SeedWork.SearchableRepository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ParkingLotAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarBrandController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarBrandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCarBrandInput input, CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(input, cancellationToken);
            return CreatedAtAction(nameof(Post), new { Id = output.Id }, new APIResponse<CarBrandModelOutput>(output));
        }

        [HttpDelete("{id::guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(new DeleteCarBrandInput(id), cancellationToken);
            return NoContent();
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
            var input = new ListCarBrandsInput();

            if (page is not null) input.Page = page.Value;
            if (perPage is not null) input.PerPage = perPage.Value;
            if (!string.IsNullOrWhiteSpace(search)) input.Search = search;
            if (!string.IsNullOrWhiteSpace(sort)) input.Sort = sort;
            if (dir is not null) input.Dir = dir.Value;

            var output = await _mediator.Send(input, cancellationToken);

            return Ok(new APIResponseList<CarBrandModelOutput>(output));
        }

        [HttpGet("{id::guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(new GetCarBrandInput(id), cancellationToken);
            return Ok(new APIResponse<CarBrandModelOutput>(output));
        }

        [HttpPut("{id::guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCarBrandAPIInput input, CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(new UpdateCarBrandInput(id, input.Name), cancellationToken);
            return Ok(new APIResponse<CarBrandModelOutput>(output));
        }
    }
}
