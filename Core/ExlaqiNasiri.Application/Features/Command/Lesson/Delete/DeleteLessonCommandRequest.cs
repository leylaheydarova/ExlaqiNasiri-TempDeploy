using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Lesson.Delete
{
    public class DeleteLessonCommandRequest : IRequest<CommandResponse>
    {
        public Guid Id { get; set; }
    }
}
