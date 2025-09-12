using ExlaqiNasiri.Application.DTOs.Authentication;

namespace ExlaqiNasiri.Application.Features.Command.Authentication.UpdateToken
{
    public class UpdateAccessTokenCommandResponse : CommandResponse
    {
        public TokenDto Dto { get; set; }
    }
}
