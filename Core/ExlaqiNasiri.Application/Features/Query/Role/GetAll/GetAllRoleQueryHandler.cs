using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.Role;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.Role.GetAll
{
    public class GetAllRoleQueryHandler : IRequestHandler<GetAllRolesQueryRequest, GetAllQueryResponse<RoleGetDto>>
    {
        readonly IRoleService _service;

        public GetAllRoleQueryHandler(IRoleService service)
        {
            _service = service;
        }

        public async Task<GetAllQueryResponse<RoleGetDto>> Handle(GetAllRolesQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.GetAllRolesAsync();
            if (!result.IsSuccess) return new GetAllQueryResponse<RoleGetDto>()
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message
            };
            return new GetAllQueryResponse<RoleGetDto>()
            {
                TotalCount = result.Value.Count,
                Data = result.Value
            };

        }
    }
}
