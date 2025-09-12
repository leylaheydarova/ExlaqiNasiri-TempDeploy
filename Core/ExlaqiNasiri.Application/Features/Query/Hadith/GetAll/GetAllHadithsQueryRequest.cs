using ExlaqiNasiri.Application.DTOs.Hadith;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.Hadith.GetAll
{
    public class GetAllHadithsQueryRequest : IRequest<GetAllQueryResponse<HadithGetAllDto>>
    {
        public HadithFilterDto? Dto { get; set; }
    }
}
