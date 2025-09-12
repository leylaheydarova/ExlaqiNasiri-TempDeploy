using ExlaqiNasiri.Application.DTOs.Article;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Article.Update
{
    public class UpdateArticleCommandRequest : IRequest<CommandResponse>
    {
        public Guid Id { get; set; }
        public ArticlePatchDto Dto { get; set; }
    }
}
