using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.HadithCategory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Command.HadithCategory.Update
{
    public class UpdateHadithCategoryCommandHandler : IRequestHandler<UpdateHadithCategoryCommandRequest, CommandResponse>
    {
        readonly IHadithCategoryService _service;
        public UpdateHadithCategoryCommandHandler(IHadithCategoryService service)
        {
            _service = service;
        }
        public async Task<CommandResponse> Handle(UpdateHadithCategoryCommandRequest request, CancellationToken cancellationToken)
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
                Message = "Category was updated successfully",
                Result = true
            };
        }
    }
}
