using ExlaqiNasiri.Application.DTOs.WebNews;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.WebNews.GetSingle
{
    public class GetSingleWebNewsQueryRequest : IRequest<GetSingleQueryResponse<WebNewsGetSingleDto>>
    {
        public Guid Id { get; set; }
    }
}
