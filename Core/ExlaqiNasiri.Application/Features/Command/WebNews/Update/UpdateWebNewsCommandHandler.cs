using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Features.Command.WebNews.Delete;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Command.WebNews.Update
{
    public class UpdateWebNewsCommandHandler : IRequestHandler<UpdateWebNewsCommandRequest, CommandResponse>
    {
        readonly IWebNewsService _service;

        public UpdateWebNewsCommandHandler(IWebNewsService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(UpdateWebNewsCommandRequest request, CancellationToken cancellationToken)
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
                Message = "News was updated successfully",
                Result = true
            };
        }
    }
}

