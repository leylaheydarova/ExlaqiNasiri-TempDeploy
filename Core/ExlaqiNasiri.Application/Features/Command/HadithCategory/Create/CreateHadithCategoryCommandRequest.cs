using ExlaqiNasiri.Application.DTOs.HadithCategory;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.HadithCategory.Create
{
    public class CreateHadithCategoryCommandRequest : IRequest<CommandResponse>
    {
        public HadithCategoryCommandDTO Dto { get; set; }
    }
}
