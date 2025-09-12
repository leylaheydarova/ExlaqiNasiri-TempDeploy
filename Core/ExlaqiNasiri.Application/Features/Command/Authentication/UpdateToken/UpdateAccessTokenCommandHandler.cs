using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Authentication.UpdateToken
{
    public class UpdateAccessTokenCommandHandler : IRequestHandler<UpdateAccessTokenCommandRequest, UpdateAccessTokenCommandResponse>
    {
        readonly IAuthenticationService _service;

        public UpdateAccessTokenCommandHandler(IAuthenticationService service)
        {
            _service = service;
        }

        public async Task<UpdateAccessTokenCommandResponse> Handle(UpdateAccessTokenCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.UpdateAccessToken(request.RefreshToken);
            if (!result.IsSuccess) return new UpdateAccessTokenCommandResponse
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message,
                Result = false
            };
            return new UpdateAccessTokenCommandResponse
            {
                Message = "Access token was updated successfully",
                Result = true,
                Dto = result.Value
            };
        }
    }
}
