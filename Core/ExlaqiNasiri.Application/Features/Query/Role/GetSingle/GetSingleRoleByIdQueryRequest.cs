using ExlaqiNasiri.Application.DTOs.Role;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.Role.GetSingle
{
    public class GetSingleRoleByIdQueryRequest : IRequest<GetSingleQueryResponse<RoleGetDto>>
    {
        public string Id { get; set; }
    }
}
