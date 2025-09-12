using ExlaqiNasiri.Application.DTOs.WebUser;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.WebUser.GetAll
{
    public class GetAllWebUserQueryRequest : IRequest<GetAllQueryResponse<WebUserGetDto>>
    {
        public DateTime? LastCreatedAt { get; set; }
        public int Size { get; set; } = 10;
    }
}
