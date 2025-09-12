using ExlaqiNasiri.Application.DTOs.LessonField;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.LessonField.Create
{
    public class CreateLessonFieldCommandRequest : IRequest<CommandResponse>
    {
        public LessonFieldCommandDto dto { get; set; }
    }
}
