using ExlaqiNasiri.Application.Features.Command.User.ChangePassword;
using ExlaqiNasiri.Application.Features.Command.User.RemoveAccount;
using ExlaqiNasiri.Application.Features.Command.User.VerifyEmail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExlaqiNasiri.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmailAsync([FromBody] VerifyEmailUserCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);

        }
        [HttpDelete]
        [Authorize(Policy = "GeneralUsePolicy")]
        public async Task<IActionResult> RemoveAccountAsync(RemoveAccountCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch]
        [Authorize(Policy = "GeneralUsePolicy")]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

    }
}
