using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.LessonField.Delete
{
    public class DeleteLessonFieldCommandRequest : IRequest<CommandResponse>
    {
        public Guid Id { get; set; }
    }
}
