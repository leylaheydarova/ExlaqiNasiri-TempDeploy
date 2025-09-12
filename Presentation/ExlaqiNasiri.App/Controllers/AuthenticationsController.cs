using ExlaqiNasiri.Application.DTOs.Authentication;
using ExlaqiNasiri.Application.Features.Command.Authentication.ForgotPassword;
using ExlaqiNasiri.Application.Features.Command.Authentication.Login;
using ExlaqiNasiri.Application.Features.Command.Authentication.Logout;
using ExlaqiNasiri.Application.Features.Command.Authentication.ResendCode;
using ExlaqiNasiri.Application.Features.Command.Authentication.ResetPassword;
using ExlaqiNasiri.Application.Features.Command.Authentication.UpdateToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExlaqiNasiri.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthenticationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync([FromForm] LoginUserCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("access-token")]
        [Authorize(Policy = "GeneralUsePolicy")]
        public async Task<IActionResult> UpdateAccessToken(UpdateAccessTokenCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("logout")]
        [Authorize(Policy = "GeneralUsePolicy")]
        public async Task<IActionResult> LogoutAsync(LogoutCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("forgot-password")]
        [Authorize(Policy = "GeneralUsePolicy")]
        public async Task<IActionResult> ForgotPasswordAsync([FromQuery] string email)
        {
            var request = new ForgotPasswordCommandRequest
            {
                Email = email
            };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("reset-password")]
        [Authorize(Policy = "GeneralUsePolicy")]
        public async Task<IActionResult> ResetPasswordAsync([FromQuery] string email, ResetPasswordDto dto)
        {

            var request = new ResetPasswordCommandRequest
            {
                dto = dto
            };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("otp-code")]
        [Authorize(Policy = "GeneralUsePolicy")]
        public async Task<IActionResult> ResendOtpCodeAsync([FromQuery] ResendCodeCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }
    }
}
