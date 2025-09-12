using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Authentication.Logout
{
    public class LogoutCommandRequest : IRequest<CommandResponse>
    {
        public string RefreshToken { get; set; }
    }
}
