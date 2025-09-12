using ExlaqiNasiri.Application.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Command.Article.Cretate
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommondRequest, CommandResponse>
    {
        readonly IArticleService _service;

        public CreateArticleCommandHandler(IArticleService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(CreateArticleCommondRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.CreateAsync(request.Dto);

            if (result.IsFailure) return new CommandResponse
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message,
                Result = false
            };

            return new CommandResponse()
            {
                StatusCode = 201,
                Message = "Article was created successfully!",
                Result = true
            };
        }
    }
}
