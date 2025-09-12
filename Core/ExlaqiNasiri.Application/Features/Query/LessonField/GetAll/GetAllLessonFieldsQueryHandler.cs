using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.LessonField;
using ExlaqiNasiri.Application.Features.Query.GetAll;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.LessonField.GetAll
{
    public class GetAllLessonFieldsQueryHandler : IRequestHandler<GetAllLessonFieldsQueryRequest, GetAllQueryResponse<LessonFieldGetDto>>
    {
        readonly ILessonFieldService _service;

        public GetAllLessonFieldsQueryHandler(ILessonFieldService service)
        {
            _service = service;
        }

        public async Task<GetAllQueryResponse<LessonFieldGetDto>> Handle(GetAllLessonFieldsQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.GetAllAsync(request.dto);
            if (result.IsFailure) return new GetAllQueryResponse<LessonFieldGetDto>
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message
            };
            return new GetAllQueryResponse<LessonFieldGetDto>
            {
                Data = result.Value,
                TotalCount = result.Value.Count
            };
        }
    }
}
