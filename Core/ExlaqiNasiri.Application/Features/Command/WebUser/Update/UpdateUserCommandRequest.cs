using ExlaqiNasiri.Application.DTOs.WebUser;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.WebUser.Update
{
    public class UpdateUserCommandRequest : IRequest<CommandResponse>
    {
        public string UserIdOrEmail { get; set; }
        public WebUserPatchDto dto { get; set; }
    }
}
