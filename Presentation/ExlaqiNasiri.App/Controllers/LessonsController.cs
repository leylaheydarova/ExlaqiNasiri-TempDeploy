using ExlaqiNasiri.Application.Features.Command.Lesson.Create;
using ExlaqiNasiri.Application.Features.Command.Lesson.Delete;
using ExlaqiNasiri.Application.Features.Command.Lesson.Update;
using ExlaqiNasiri.Application.Features.Query.Lesson.GetAll;
using ExlaqiNasiri.Application.Features.Query.Lesson.GetSingle;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExlaqiNasiri.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        readonly IMediator _mediator;

        public LessonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllLessonQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateAsync([FromForm] CreateLessonCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var request = new DeleteLessonCommandRequest { Id = id };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateLessonCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "GeneralUsePolicy")]
        public async Task<IActionResult> GetSingleAsync([FromRoute] Guid id)
        {
            var request = new GetSingleLessonQueryRequest
            {
                Id = id
            };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }
    }
}
