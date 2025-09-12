using ExlaqiNasiri.Application.DTOs.Hadith;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Hadith.Upload
{
    public class UpdateHadithCommandRequest : IRequest<CommandResponse>
    {
        public Guid Id { get; set; }
        public HadithPatchDto Dto { get; set; }
    }
}
