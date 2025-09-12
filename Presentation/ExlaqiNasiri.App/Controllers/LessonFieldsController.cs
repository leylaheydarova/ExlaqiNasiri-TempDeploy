using ExlaqiNasiri.Application.DTOs.LessonField;
using ExlaqiNasiri.Application.Features.Command.LessonField.Create;
using ExlaqiNasiri.Application.Features.Command.LessonField.Delete;
using ExlaqiNasiri.Application.Features.Command.LessonField.Toggle;
using ExlaqiNasiri.Application.Features.Command.LessonField.Update;
using ExlaqiNasiri.Application.Features.Query.GetAll;
using ExlaqiNasiri.Application.Features.Query.LessonField.GetSingle;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExlaqiNasiri.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonFieldsController : ControllerBase
    {
        readonly IMediator _mediator;
        public LessonFieldsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllLessonFieldsQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleAsync([FromRoute] Guid id)
        {
            var request = new GetSingleLessonFieldQueryRequest
            {
                Id = id
            };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateAsync(CreateLessonFieldCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var request = new DeleteLessonFieldCommandRequest
            {
                Id = id
            };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("toggle/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> ToggleAsync([FromRoute] Guid id)
        {
            var request = new ToggleLessonFieldCommandRequest
            {
                Id = id
            };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] LessonFieldPatchDto dto)
        {
            var request = new UpdateLessonFieldCommandRequest
            {
                Id = id,
                Dto = dto
            };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

    }
}
