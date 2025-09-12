using ExlaqiNasiri.Application.DTOs.Article;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Query.Article.GetAll
{
    public class GetAllArticleQueryRequest : IRequest<GetAllQueryResponse<ArticleGetAllDto>>
    {
        public ArticleFilterDto? Dto { get; set; }
    }
}
