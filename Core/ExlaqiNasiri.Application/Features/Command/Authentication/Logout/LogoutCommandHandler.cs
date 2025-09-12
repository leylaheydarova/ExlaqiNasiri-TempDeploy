using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Authentication.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommandRequest, CommandResponse>
    {
        readonly IAuthenticationService _service;

        public LogoutCommandHandler(IAuthenticationService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(LogoutCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.LogoutAsync(request.RefreshToken);
            if (!result.IsSuccess) return new CommandResponse
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message,
                Result = false
            };

            return new CommandResponse
            {
                Message = "Logout was successfully finished!",
                Result = true
            };
        }
    }
}
