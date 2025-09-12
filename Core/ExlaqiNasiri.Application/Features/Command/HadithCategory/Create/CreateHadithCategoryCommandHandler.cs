using ExlaqiNasiri.Application.Abstraction;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.HadithCategory.Create
{
    public class CreateHadithCategoryCommandHandler : IRequestHandler<CreateHadithCategoryCommandRequest, CommandResponse>
    {
        readonly IHadithCategoryService _service;

        public CreateHadithCategoryCommandHandler(IHadithCategoryService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(CreateHadithCategoryCommandRequest request, CancellationToken cancellationToken)
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
                Message = "Category was created successfully!",
                Result = true
            };
        }
    }
}
