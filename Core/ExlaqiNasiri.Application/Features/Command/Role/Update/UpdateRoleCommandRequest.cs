using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Role.Update
{
    public class UpdateRoleCommandRequest : IRequest<CommandResponse>
    {
        public string Id { get; set; }
        public string roleName { get; set; }
    }
}
