using ExlaqiNasiri.Application.Features.Command.Role.Create;
using ExlaqiNasiri.Application.Features.Command.Role.Delete;
using ExlaqiNasiri.Application.Features.Command.Role.Update;
using ExlaqiNasiri.Application.Features.Query.Role.GetAll;
using ExlaqiNasiri.Application.Features.Query.Role.GetSingle;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExlaqiNasiri.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetAllRolesAsync([FromQuery] GetAllRolesQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetRoleByIdAsync(string id)
        {
            var request = new GetSingleRoleByIdQueryRequest
            {
                Id = id
            };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateRoleAsync(CreateRoleCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteRoleAsync(string id)
        {
            var request = new DeleteRoleCommandRequest
            {
                Id = id
            };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateRoleAsync(UpdateRoleCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }
    }
}
