using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.LessonField.Toggle
{
    public class ToggleLessonFieldCommandHandler : IRequestHandler<ToggleLessonFieldCommandRequest, CommandResponse>
    {
        readonly ILessonFieldService _service;

        public ToggleLessonFieldCommandHandler(ILessonFieldService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(ToggleLessonFieldCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.ToggleAsync(request.Id);
            if (result.IsFailure) return new CommandResponse
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message,
                Result = false
            };

            string message = result.Value ? "Lesson field was deleted successfully" : "Lesson field was restored successfully";

            return new CommandResponse
            {
                Message = message,
                Result = result.Value
            };
        }
    }
}
