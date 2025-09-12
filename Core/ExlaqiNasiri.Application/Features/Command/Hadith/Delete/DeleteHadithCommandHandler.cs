using ExlaqiNasiri.Application.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Command.Hadith.Delete
{
    public class DeleteHadithCommandHandler : IRequestHandler<DeleteHadithCommandRequest, CommandResponse>
    {
        readonly IHadithService _service;

        public DeleteHadithCommandHandler(IHadithService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(DeleteHadithCommandRequest request, CancellationToken cancellationToken)
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
                Message = "Hadith was deleted successfully",
                Result = true
            };
        }
    }
}
