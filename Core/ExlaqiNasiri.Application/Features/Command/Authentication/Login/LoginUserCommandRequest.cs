using ExlaqiNasiri.Application.DTOs.Authentication;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Authentication.Login
{
    public class LoginUserCommandRequest : CreateCommandRequest<LoginDto>, IRequest<LoginUserCommandResponse>
    {

    }
}
