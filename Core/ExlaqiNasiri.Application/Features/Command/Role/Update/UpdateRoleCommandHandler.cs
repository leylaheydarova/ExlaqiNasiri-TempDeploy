using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Role.Update
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommandRequest, CommandResponse>
    {
        readonly IRoleService _service;

        public UpdateRoleCommandHandler(IRoleService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.UpdateRoleAsync(request.Id, request.roleName);
            if (!result.IsSuccess) return new CommandResponse
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message,
                Result = false
            };
            return new CommandResponse
            {
                Message = "Role was updated successfully!",
                Result = true
            };
        }
    }
}
