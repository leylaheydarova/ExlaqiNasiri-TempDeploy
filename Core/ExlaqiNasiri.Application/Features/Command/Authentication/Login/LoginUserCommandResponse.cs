using ExlaqiNasiri.Application.DTOs.Authentication;

namespace ExlaqiNasiri.Application.Features.Command.Authentication.Login
{
    public class LoginUserCommandResponse : CommandResponse
    {
        public TokenDto? Dto { get; set; }
    }
}
