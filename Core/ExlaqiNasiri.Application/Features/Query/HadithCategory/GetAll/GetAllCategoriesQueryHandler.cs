using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.HadithCategory;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.HadithCategory.GetAll
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQueryRequest, GetAllQueryResponse<HadithCategoryGetDto>>
    {
        readonly IHadithCategoryService _service;

        public GetAllCategoriesQueryHandler(IHadithCategoryService service)
        {
            _service = service;
        }

        public async Task<GetAllQueryResponse<HadithCategoryGetDto>> Handle(GetAllCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.GetAllAsync(request.Dto);
            if (result.IsFailure) return new GetAllQueryResponse<HadithCategoryGetDto> { StatusCode = (int)result.Error.StatusCode, Message = result.Error.Message };
            return new GetAllQueryResponse<HadithCategoryGetDto>
            {
                Data = result.Value,
                TotalCount = result.Value.Count()
            };
        }
    }
}
