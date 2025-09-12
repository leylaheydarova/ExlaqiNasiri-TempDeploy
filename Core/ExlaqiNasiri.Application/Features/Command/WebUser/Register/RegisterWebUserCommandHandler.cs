using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.WebUser.Register
{
    public class RegisterWebUserCommandHandler : IRequestHandler<RegisterWebUserCommandRequest, CommandResponse>
    {
        readonly IWebUserService _service;

        public RegisterWebUserCommandHandler(IWebUserService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(RegisterWebUserCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.RegisterAsync(request.Dto);
            if (result.IsFailure) return new CommandResponse
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message,
                Result = false
            };

            return new CommandResponse
            {
                Message = " Web User was registered successfully!",
                StatusCode = 201,
                Result = true
            };
        }
    }
}
