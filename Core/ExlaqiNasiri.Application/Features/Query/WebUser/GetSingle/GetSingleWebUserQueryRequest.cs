using ExlaqiNasiri.Application.DTOs.WebUser;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.WebUser.GetSingle
{
    public class GetSingleWebUserQueryRequest : IRequest<GetSingleQueryResponse<WebUserGetDto>>
    {
        public string UserIdOrEmail { get; set; }
    }
}
