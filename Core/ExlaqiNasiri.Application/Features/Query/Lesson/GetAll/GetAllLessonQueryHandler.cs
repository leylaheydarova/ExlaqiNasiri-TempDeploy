using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.Lesson;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.Lesson.GetAll
{
    public class GetAllLessonQueryHandler : IRequestHandler<GetAllLessonQueryRequest, GetAllQueryResponse<LessonGetAllDto>>
    {
        readonly ILessonService _service;

        public GetAllLessonQueryHandler(ILessonService service)
        {
            _service = service;
        }

        public async Task<GetAllQueryResponse<LessonGetAllDto>> Handle(GetAllLessonQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.GetAllAsync(request.dto);
            if (result.IsFailure) return new GetAllQueryResponse<LessonGetAllDto>()
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message
            };
            return new GetAllQueryResponse<LessonGetAllDto>()
            {
                Data = result.Value,
                TotalCount = result.Value.Count
            };
        }
    }
}
