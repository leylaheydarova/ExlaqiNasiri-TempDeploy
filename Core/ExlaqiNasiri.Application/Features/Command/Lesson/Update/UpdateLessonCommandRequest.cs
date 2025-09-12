using ExlaqiNasiri.Application.DTOs.Lesson;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Lesson.Update
{
    public class UpdateLessonCommandRequest : IRequest<CommandResponse>
    {
        public Guid Id { get; set; }
        public LessonUpdateDto dto { get; set; }
    }
}
