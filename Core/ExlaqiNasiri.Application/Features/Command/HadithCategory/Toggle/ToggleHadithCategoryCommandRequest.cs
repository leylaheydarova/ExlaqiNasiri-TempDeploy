using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.HadithCategory.Toggle
{
    public class ToggleHadithCategoryCommandRequest : IRequest<CommandResponse>
    {
        public Guid Id { get; set; }
    }
}
