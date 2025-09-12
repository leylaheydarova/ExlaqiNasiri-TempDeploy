using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.Role;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.Role.GetSingle
{
    public class GetSingleRoleByIdQueryHandler : IRequestHandler<GetSingleRoleByIdQueryRequest, GetSingleQueryResponse<RoleGetDto>>
    {
        readonly IRoleService _service;

        public GetSingleRoleByIdQueryHandler(IRoleService service)
        {
            _service = service;
        }

        public async Task<GetSingleQueryResponse<RoleGetDto>> Handle(GetSingleRoleByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.GetRoleByIdAsync(request.Id);
            if (result.IsFailure) return new GetSingleQueryResponse<RoleGetDto>
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message
            };
            return new GetSingleQueryResponse<RoleGetDto>
            {
                Data = result.Value
            };
        }
    }
}
