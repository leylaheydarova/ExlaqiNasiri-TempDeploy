using ExlaqiNasiri.Application.DTOs.LessonField;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.LessonField.Update
{
    public class UpdateLessonFieldCommandRequest : IRequest<CommandResponse>
    {
        public Guid Id { get; set; }
        public LessonFieldPatchDto Dto { get; set; }
    }
}
