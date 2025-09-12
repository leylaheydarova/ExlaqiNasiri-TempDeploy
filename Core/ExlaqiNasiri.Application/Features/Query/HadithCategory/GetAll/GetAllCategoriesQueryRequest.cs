using ExlaqiNasiri.Application.DTOs.HadithCategory;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.HadithCategory
{
    public class GetAllCategoriesQueryRequest : IRequest<GetAllQueryResponse<HadithCategoryGetDto>>
    {
        public HadithCategoryFilterDto? Dto { get; set; }
    }
}
