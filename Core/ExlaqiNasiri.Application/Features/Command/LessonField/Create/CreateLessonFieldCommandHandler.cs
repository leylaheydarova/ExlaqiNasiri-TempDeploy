using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.LessonField.Create
{
    public class CreateLessonFieldCommandHandler : IRequestHandler<CreateLessonFieldCommandRequest, CommandResponse>
    {
        readonly ILessonFieldService _service;

        public CreateLessonFieldCommandHandler(ILessonFieldService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(CreateLessonFieldCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.CreateAsync(request.dto);

            if (result.IsFailure) return new CommandResponse
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message,
                Result = false
            };

            return new CommandResponse
            {
                StatusCode = 201,
                Message = "Lesson field was created successfully!",
                Result = true
            };
        }
    }
}
