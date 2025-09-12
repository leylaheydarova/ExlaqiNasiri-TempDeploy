using ExlaqiNasiri.Application.DTOs.WebNews;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.WebNews.Update
{
    public class UpdateWebNewsCommandRequest : IRequest<CommandResponse>
    {
        public Guid Id { get; set; }
        public WebNewsPatchDto Dto { get; set; }
    }
}
