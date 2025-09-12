using ExlaqiNasiri.Application.DTOs.Lesson;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Lesson.Create
{
    public class CreateLessonCommandRequest : IRequest<CommandResponse>
    {
        public LessonCreateDto dto { get; set; }
    }
}
