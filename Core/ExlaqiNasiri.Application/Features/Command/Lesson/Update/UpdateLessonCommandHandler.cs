using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Lesson.Update
{
    public class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommandRequest, CommandResponse>
    {
        readonly ILessonService _service;

        public UpdateLessonCommandHandler(ILessonService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(UpdateLessonCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.UpdateAsync(request.Id, request.dto);
            if (result.IsFailure) return new CommandResponse()
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message,
                Result = false
            };
            return new CommandResponse()
            {
                Message = "Lesson was updated successfully!",
                Result = true
            };

        }
    }
}
