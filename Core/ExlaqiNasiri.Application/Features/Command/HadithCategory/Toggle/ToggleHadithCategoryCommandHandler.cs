using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.HadithCategory.Toggle
{
    public class ToggleHadithCategoryCommandHandler : IRequestHandler<ToggleHadithCategoryCommandRequest, CommandResponse>
    {
        readonly IHadithCategoryService _service;

        public ToggleHadithCategoryCommandHandler(IHadithCategoryService service)
        {
            _service = service;
        }
        public async Task<CommandResponse> Handle(ToggleHadithCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.ToggleAsync(request.Id);

            if (result.IsFailure) return new CommandResponse
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message,
                Result = false
            };

            string message = result.Value ? "Category was deleted successfully" : "Category was restores successfully";

            return new CommandResponse
            {
                Message = message,
                Result = result.Value
            };
        }
    }
}
