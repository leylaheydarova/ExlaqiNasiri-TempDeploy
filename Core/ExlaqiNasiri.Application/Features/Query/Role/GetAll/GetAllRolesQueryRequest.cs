using ExlaqiNasiri.Application.DTOs.Role;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.Role.GetAll
{
    public class GetAllRolesQueryRequest : IRequest<GetAllQueryResponse<RoleGetDto>>
    {
        public RoleGetDto? dto { get; set; }
    }
}
