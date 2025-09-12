using ExlaqiNasiri.Application.DTOs.HadithCategory;
using ExlaqiNasiri.Application.Features.Command.HadithCategory.Create;
using ExlaqiNasiri.Application.Features.Command.HadithCategory.Delete;
using ExlaqiNasiri.Application.Features.Command.HadithCategory.Toggle;
using ExlaqiNasiri.Application.Features.Command.HadithCategory.Update;
using ExlaqiNasiri.Application.Features.Query.HadithCategory;
using ExlaqiNasiri.Application.Features.Query.HadithCategory.GetSingle;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExlaqiNasiri.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HadithCategoriesController : ControllerBase
    {
        readonly IMediator _mediator;

        public HadithCategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllCategoriesQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleAsync([FromRoute] Guid id)
        {
            var request = new GetSingleCategoriesQueryRequest { Id = id };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateAsync(CreateHadithCategoryCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var request = new DeleteHadithCategoryCommandRequest { Id = id };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("toggle/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> ToggleAsync([FromRoute] Guid id)
        {
            var request = new ToggleHadithCategoryCommandRequest { Id = id };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, HadithCategoryCommandDTO dto)
        {
            UpdateHadithCategoryCommandRequest request = new UpdateHadithCategoryCommandRequest
            {
                Id = id,
                Dto = dto
            };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }
    }
}

