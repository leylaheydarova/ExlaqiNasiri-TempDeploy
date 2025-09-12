using ExlaqiNasiri.Application.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ExlaqiNasiri.Application.Features.Command.User.VerifyEmail
{
    public class VerifyEmailUserCommandHandler : IRequestHandler<VerifyEmailUserCommandRequest, CommandResponse>
    {
        private readonly IUserService _userService;
        private readonly ILogger<VerifyEmailUserCommandHandler> _logger;

        public VerifyEmailUserCommandHandler(IUserService userService, ILogger<VerifyEmailUserCommandHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<CommandResponse> Handle(VerifyEmailUserCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _userService.VerifyEmailAsync(request.Email, request.OtpCode, request.Purpose);

            if (result.IsSuccess)
            {
                return new CommandResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Email verified successfully.",
                    Result = true
                };
            }

            return new CommandResponse
            {
                StatusCode = (int)result.Error.StatusCode,
                Message = result.Error.Message,
                Result = false
            };
        }
    }
}