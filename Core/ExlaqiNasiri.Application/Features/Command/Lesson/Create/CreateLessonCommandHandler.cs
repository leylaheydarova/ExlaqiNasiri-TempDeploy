using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Lesson.Create
{
    public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommandRequest, CommandResponse>
    {
        readonly ILessonService _service;

        public CreateLessonCommandHandler(ILessonService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(CreateLessonCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.CreateAsync(request.dto);
            if (result.IsFailure) return new CommandResponse
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message,
                Result = false
            };
            return new CommandResponse()
            {
                StatusCode = 201,
                Message = "Lesson was created successfully!",
                Result = true
            };
        }
    }
}
