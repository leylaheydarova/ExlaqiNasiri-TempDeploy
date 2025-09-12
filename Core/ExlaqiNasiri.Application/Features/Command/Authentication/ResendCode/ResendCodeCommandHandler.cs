using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Authentication.ResendCode
{
    public class ResendCodeCommandHandler : IRequestHandler<ResendCodeCommandRequest, CommandResponse>
    {
        readonly IAuthenticationService _service;

        public ResendCodeCommandHandler(IAuthenticationService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(ResendCodeCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.ResendOtpCodeAsync(request.UserIdOrEmail);
            if (!result.IsSuccess) return new CommandResponse
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message,
                Result = false
            };
            return new CommandResponse
            {
                Message = "Otp code was sent successfully!",
                Result = true
            };

        }
    }
}
