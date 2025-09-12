using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.LessonField.Toggle
{
    public class ToggleLessonFieldCommandRequest : IRequest<CommandResponse>
    {
        public Guid Id { get; set; }
    }
}
