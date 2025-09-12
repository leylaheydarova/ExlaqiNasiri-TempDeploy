using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.LessonField.Update
{
    public class UpdateLessonFieldCommandHandler : IRequestHandler<UpdateLessonFieldCommandRequest, CommandResponse>
    {
        readonly ILessonFieldService _service;

        public UpdateLessonFieldCommandHandler(ILessonFieldService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(UpdateLessonFieldCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.UpdateAsync(request.Id, request.Dto);
            if (result.IsFailure) return new CommandResponse
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message,
                Result = false
            };

            return new CommandResponse
            {
                Message = "Lesson field was updated successfully!",
                Result = true
            };
        }
    }
}
