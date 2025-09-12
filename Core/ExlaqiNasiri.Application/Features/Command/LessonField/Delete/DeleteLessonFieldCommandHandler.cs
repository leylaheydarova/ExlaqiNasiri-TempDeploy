using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.LessonField.Delete
{
    public class DeleteLessonFieldCommandHandler : IRequestHandler<DeleteLessonFieldCommandRequest, CommandResponse>
    {
        readonly ILessonFieldService _service;

        public DeleteLessonFieldCommandHandler(ILessonFieldService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(DeleteLessonFieldCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.DeleteAsync(request.Id);

            if (result.IsFailure) return new CommandResponse
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message,
                Result = false
            };

            return new CommandResponse
            {
                Result = true,
                Message = "Lesson field was deleted successfully!"
            };
        }
    }
}
