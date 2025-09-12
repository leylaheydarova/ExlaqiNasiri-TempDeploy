using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Role.Create
{
    public class CreateRoleCommandRequest : IRequest<CommandResponse>
    {
        public string RoleName { get; set; }
    }
}
