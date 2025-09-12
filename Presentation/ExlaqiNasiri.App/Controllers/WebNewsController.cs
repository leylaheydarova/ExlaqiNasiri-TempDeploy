using ExlaqiNasiri.Application.Features.Command.WebNews.Create;
using ExlaqiNasiri.Application.Features.Command.WebNews.Delete;
using ExlaqiNasiri.Application.Features.Command.WebNews.Update;
using ExlaqiNasiri.Application.Features.Query.WebNews.GetAll;
using ExlaqiNasiri.Application.Features.Query.WebNews.GetSingle;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExlaqiNasiri.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebNewsController : ControllerBase
    {
        readonly IMediator _mediator;

        public WebNewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllWebNewsQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateAsync([FromForm] CreateWebNewsCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var request = new DeleteWebNewsCommandRequest { Id = id };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateWebNewsCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleAsync([FromRoute] Guid id)
        {
            var request = new GetSingleWebNewsQueryRequest { Id = id };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }
    }
}
