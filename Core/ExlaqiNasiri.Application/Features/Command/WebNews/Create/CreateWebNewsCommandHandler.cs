using ExlaqiNasiri.Application.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Command.WebNews.Create
{
    public class CreateWebNewsCommandHandler : IRequestHandler<CreateWebNewsCommandRequest, CommandResponse>
    {
        readonly IWebNewsService _service;

        public CreateWebNewsCommandHandler(IWebNewsService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(CreateWebNewsCommandRequest request, CancellationToken cancellationToken)
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
                Message = "News was created successfully!",
                Result = true
            };
        }
    }
}
