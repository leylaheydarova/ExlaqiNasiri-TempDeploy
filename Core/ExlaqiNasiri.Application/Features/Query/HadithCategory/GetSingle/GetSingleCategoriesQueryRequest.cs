using ExlaqiNasiri.Application.DTOs.HadithCategory;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.HadithCategory.GetSingle
{
    public class GetSingleCategoriesQueryRequest : IRequest<GetSingleQueryResponse<HadithCategoryGetDto>>
    {
        public Guid Id { get; set; }
    }
}
