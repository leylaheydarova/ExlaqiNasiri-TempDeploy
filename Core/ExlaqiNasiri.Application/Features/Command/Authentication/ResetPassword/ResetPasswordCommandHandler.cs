using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Authentication.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommandRequest, CommandResponse>
    {
        readonly IAuthenticationService _service;

        public ResetPasswordCommandHandler(IAuthenticationService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(ResetPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.ResetPasswordAsync(request.dto);
            if (!result.IsSuccess) return new CommandResponse
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message,
                Result = false
            };
            return new CommandResponse
            {
                StatusCode = 201,
                Message = "Password was resetted successfully",
                Result = true
            };
        }
    }
}
