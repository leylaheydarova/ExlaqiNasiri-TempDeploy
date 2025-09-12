using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Lesson.Delete
{
    public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommandRequest, CommandResponse>
    {
        readonly ILessonService _service;

        public DeleteLessonCommandHandler(ILessonService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(DeleteLessonCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.DeleteAsync(request.Id);
            if (result.IsFailure) return new CommandResponse()
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message,
                Result = false
            };
            return new CommandResponse()
            {
                Message = "Lesson was deleted successfully!",
                Result = true
            };
        }
    }
}
