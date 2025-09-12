using ExlaqiNasiri.Application.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Command.Hadith.Upload
{
    public class UpdateHadithCommandHandler : IRequestHandler<UpdateHadithCommandRequest, CommandResponse>
    {
        readonly IHadithService _service;

        public UpdateHadithCommandHandler(IHadithService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(UpdateHadithCommandRequest request, CancellationToken cancellationToken)
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
                Message = "Hadith was updated successfully",
                Result = true
            };
        }
    }
}
