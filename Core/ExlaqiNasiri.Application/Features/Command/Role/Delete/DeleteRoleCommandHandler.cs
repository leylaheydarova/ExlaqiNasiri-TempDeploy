using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Role.Delete
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommandRequest, CommandResponse>
    {
        readonly IRoleService _service;

        public DeleteRoleCommandHandler(IRoleService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.RemoveRoleAsync(request.Id);
            if (!result.IsSuccess) return new CommandResponse
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message,
                Result = false
            };
            return new CommandResponse
            {
                Message = "Role was removed successfully!",
                Result = true
            };
        }
    }
}
