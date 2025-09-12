using ExlaqiNasiri.Application.DTOs.Lesson;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.Lesson.GetSingle
{
    public class GetSingleLessonQueryRequest : IRequest<GetSingleQueryResponse<LessonGetSingleDto>>
    {
        public Guid Id { get; set; }
    }
}
