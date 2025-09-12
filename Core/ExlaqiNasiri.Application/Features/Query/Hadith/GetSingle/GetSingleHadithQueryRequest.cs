using ExlaqiNasiri.Application.DTOs.Hadith;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.Hadith.GetSingle
{
    public class GetSingleHadithQueryRequest : IRequest<GetSingleQueryResponse<HadithGetSingleDto>>
    {
        public Guid Id { get; set; }
    }
}
