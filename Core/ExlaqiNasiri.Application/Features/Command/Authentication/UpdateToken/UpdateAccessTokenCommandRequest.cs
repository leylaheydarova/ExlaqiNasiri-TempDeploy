using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Authentication.UpdateToken
{
    public class UpdateAccessTokenCommandRequest : IRequest<UpdateAccessTokenCommandResponse>
    {
        public string RefreshToken { get; set; }
    }
}
