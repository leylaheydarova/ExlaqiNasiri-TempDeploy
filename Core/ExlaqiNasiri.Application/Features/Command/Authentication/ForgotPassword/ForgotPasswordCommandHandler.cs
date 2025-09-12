using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Authentication.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommandRequest, CommandResponse>
    {
        readonly IAuthenticationService _service;

        public ForgotPasswordCommandHandler(IAuthenticationService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(ForgotPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.ForgotPasswordAsync(request.Email);
            if (!result.IsSuccess) return new CommandResponse
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message,
                Result = false
            };
            return new CommandResponse
            {
                Message = "Password was resetted successfully!",
                Result = true
            };
        }
    }
}
