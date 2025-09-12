using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.HadithCategory.Delete
{
    public class DeleteHadithCategoryCommandHandler : IRequestHandler<DeleteHadithCategoryCommandRequest, CommandResponse>
    {
        readonly IHadithCategoryService _service;

        public DeleteHadithCategoryCommandHandler(IHadithCategoryService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(DeleteHadithCategoryCommandRequest request, CancellationToken cancellationToken)
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
                Message = "Category was deleted successfully",
                Result = true
            };
        }
    }
}
