using ExlaqiNasiri.Application.DTOs.Article;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Command.Article.Cretate
{
    public class CreateArticleCommondRequest : IRequest<CommandResponse>
    {
        public ArticleCommandDto Dto { get; set; }
    }
}
