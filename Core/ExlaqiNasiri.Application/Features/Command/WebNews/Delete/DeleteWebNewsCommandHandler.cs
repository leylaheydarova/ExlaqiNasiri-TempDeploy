using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.WebNews.Delete
{
    public class DeleteWebNewsCommandHandler : IRequestHandler<DeleteWebNewsCommandRequest, CommandResponse>
    {
        readonly IWebNewsService _service;

        public DeleteWebNewsCommandHandler(IWebNewsService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(DeleteWebNewsCommandRequest request, CancellationToken cancellationToken)
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
                Message = "News was deleted successfully",
                Result = true
            };
        }
    }
}
