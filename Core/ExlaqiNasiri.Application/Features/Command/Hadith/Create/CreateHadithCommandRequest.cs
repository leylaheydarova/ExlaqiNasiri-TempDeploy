using ExlaqiNasiri.Application.DTOs.Hadith;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Hadith.Create
{
    public class CreateHadithCommandRequest : IRequest<CommandResponse>
    {
        public HadithCreateDto Dto { get; set; }
    }
}
