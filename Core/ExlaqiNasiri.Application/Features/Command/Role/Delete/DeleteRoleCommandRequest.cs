using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Role.Delete
{
    public class DeleteRoleCommandRequest : IRequest<CommandResponse>
    {
        public string Id { get; set; }
    }
}
