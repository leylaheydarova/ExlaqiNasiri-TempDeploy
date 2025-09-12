using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Role.Create
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommandRequest, CommandResponse>
    {
        readonly IRoleService _roleService;

        public CreateRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<CommandResponse> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _roleService.CreateRoleAsync(request.RoleName);
            if (!result.IsSuccess) return new CommandResponse
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message,
                Result = false
            };
            return new CommandResponse
            {
                StatusCode = 201,
                Message = "Role was created successfully!"
            };
        }
    }
}
