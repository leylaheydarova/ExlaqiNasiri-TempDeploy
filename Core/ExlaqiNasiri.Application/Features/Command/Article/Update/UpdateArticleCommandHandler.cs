using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Article.Update
{
    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommandRequest, CommandResponse>
    {
        readonly IArticleService _service;

        public UpdateArticleCommandHandler(IArticleService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(UpdateArticleCommandRequest request, CancellationToken cancellationToken)
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
                Message = "Article was updated successfully",
                Result = true
            };
        }
    }
}
