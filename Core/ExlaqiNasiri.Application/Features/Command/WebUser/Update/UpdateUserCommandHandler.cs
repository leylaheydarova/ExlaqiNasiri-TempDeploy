using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.WebUser.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, CommandResponse>
    {
        readonly IWebUserService _webUserService;

        public UpdateUserCommandHandler(IWebUserService webUserService)
        {
            _webUserService = webUserService;
        }

        public async Task<CommandResponse> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _webUserService.UpdateUserAsync(request.UserIdOrEmail, request.dto);
            if (!result.IsSuccess) return new CommandResponse
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message,
                Result = false
            };
            return new CommandResponse
            {
                Message = "User information was updated successfully!",
                Result = true
            };
        }
    }
}
