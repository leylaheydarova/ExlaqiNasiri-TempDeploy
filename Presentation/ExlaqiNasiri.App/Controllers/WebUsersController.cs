using ExlaqiNasiri.Application.DTOs.WebUser;
using ExlaqiNasiri.Application.Features.Command.WebUser.Register;
using ExlaqiNasiri.Application.Features.Command.WebUser.Update;
using ExlaqiNasiri.Application.Features.Query.WebUser.GetAll;
using ExlaqiNasiri.Application.Features.Query.WebUser.GetSingle;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExlaqiNasiri.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebUsersController : ControllerBase
    {
        readonly IMediator _mediator;

        public WebUsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync([FromQuery] GetAllWebUserQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleUserAsync(string userIdOrEmail)
        {
            var request = new GetSingleWebUserQueryRequest
            {
                UserIdOrEmail = userIdOrEmail
            };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);

        }

        [HttpPost]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> RegisterUserAsync([FromForm] RegisterWebUserCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("{userIdOrEmail}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> UpdateUserAsync(string userIdOrEmail, WebUserPatchDto dto)
        {
            var request = new UpdateUserCommandRequest
            {
                UserIdOrEmail = userIdOrEmail,
                dto = dto
            };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }
    }
}
