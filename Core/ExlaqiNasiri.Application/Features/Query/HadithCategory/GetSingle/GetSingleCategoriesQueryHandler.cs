using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.HadithCategory;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.HadithCategory.GetSingle
{
    public class GetSingleCategoriesQueryHandler : IRequestHandler<GetSingleCategoriesQueryRequest, GetSingleQueryResponse<HadithCategoryGetDto>>
    {
        readonly IHadithCategoryService _service;

        public GetSingleCategoriesQueryHandler(IHadithCategoryService service)
        {
            _service = service;
        }

        public async Task<GetSingleQueryResponse<HadithCategoryGetDto>> Handle(GetSingleCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.GetSingleAsync(request.Id);
            if (result.IsFailure) return new GetSingleQueryResponse<HadithCategoryGetDto> { StatusCode = (int)result.Error.StatusCode, Message = result.Error.Message };
            return new GetSingleQueryResponse<HadithCategoryGetDto>
            {
                Data = result.Value
            };
        }
    }
}
