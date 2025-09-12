using ExlaqiNasiri.Application.DTOs.Article;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Query.Article.GetSingle
{
    public class GetSingleArticleQueryRequest : IRequest<GetSingleQueryResponse<ArticleGetSingleDto>>
    {
        public Guid Id { get; set; }
    }
}
