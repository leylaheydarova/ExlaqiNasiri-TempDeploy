using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Hadith.Create
{
    internal class CreateHadithCommandHandler : IRequestHandler<CreateHadithCommandRequest, CommandResponse>
    {
        readonly IHadithService _service;

        public CreateHadithCommandHandler(IHadithService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(CreateHadithCommandRequest request, CancellationToken cancellationToken)
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
                Message = "Hadith was created successfully!",
                Result = true
            };
        }
    }
}
