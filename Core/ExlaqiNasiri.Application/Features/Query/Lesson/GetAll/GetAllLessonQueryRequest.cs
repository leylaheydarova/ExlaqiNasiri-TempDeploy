using ExlaqiNasiri.Application.DTOs.Lesson;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.Lesson.GetAll
{
    public class GetAllLessonQueryRequest : IRequest<GetAllQueryResponse<LessonGetAllDto>>
    {
        public LessonFilterDto? dto { get; set; }
    }
}
