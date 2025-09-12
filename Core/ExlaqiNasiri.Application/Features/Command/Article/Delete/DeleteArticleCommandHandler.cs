using ExlaqiNasiri.Application.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Command.Article.Delete
{
    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommandRequest, CommandResponse>
    {
        readonly IArticleService _service;

        public DeleteArticleCommandHandler(IArticleService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(DeleteArticleCommandRequest request, CancellationToken cancellationToken)
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
                Message = "Article was deleted successfully",
                Result = true
            };
        }
    }
}
