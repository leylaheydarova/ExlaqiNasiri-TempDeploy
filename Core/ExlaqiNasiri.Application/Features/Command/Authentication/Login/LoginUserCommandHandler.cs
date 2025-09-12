using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Authentication.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IAuthenticationService _authService;

        public LoginUserCommandHandler(IAuthenticationService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var dto = await _authService.LoginUserAsync(request.Dto);
            if (dto.IsFailure)
            {
                return new LoginUserCommandResponse
                {
                    Dto = null,
                    StatusCode = ((int)dto.Error.StatusCode),
                    Message = dto.Error.Message,
                    Result = false
                };
            }
            return new LoginUserCommandResponse
            {
                Dto = dto.Value
            };
        }
    }
}
