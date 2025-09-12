using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.LessonField;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.LessonField.GetSingle
{
    public class GetSingleLessonFieldQueryHandler : IRequestHandler<GetSingleLessonFieldQueryRequest, GetSingleQueryResponse<LessonFieldGetDto>>
    {
        readonly ILessonFieldService _service;

        public GetSingleLessonFieldQueryHandler(ILessonFieldService service)
        {
            _service = service;
        }

        public async Task<GetSingleQueryResponse<LessonFieldGetDto>> Handle(GetSingleLessonFieldQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.GetSingleAsync(request.Id);
            if (result.IsFailure) return new GetSingleQueryResponse<LessonFieldGetDto>
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message
            };
            return new GetSingleQueryResponse<LessonFieldGetDto>
            {
                Data = result.Value
            };
        }
    }
}
