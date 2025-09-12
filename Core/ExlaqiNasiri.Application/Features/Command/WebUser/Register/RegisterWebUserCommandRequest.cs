using ExlaqiNasiri.Application.DTOs.WebUser;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.WebUser.Register
{
    public class RegisterWebUserCommandRequest : CreateCommandRequest<WebUserRegisterDto>, IRequest<CommandResponse>
    {
    }
}
