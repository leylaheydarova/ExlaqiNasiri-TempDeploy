using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.Lesson;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.Lesson.GetSingle
{
    public class GetSingleLessonQueryHandler : IRequestHandler<GetSingleLessonQueryRequest, GetSingleQueryResponse<LessonGetSingleDto>>
    {
        readonly ILessonService _service;

        public GetSingleLessonQueryHandler(ILessonService service)
        {
            _service = service;
        }

        public async Task<GetSingleQueryResponse<LessonGetSingleDto>> Handle(GetSingleLessonQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.GetSingleAsync(request.Id);
            if (result.IsFailure) return new GetSingleQueryResponse<LessonGetSingleDto>()
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message
            };
            return new GetSingleQueryResponse<LessonGetSingleDto>()
            {
                Data = result.Value
            };
        }
    }
}
