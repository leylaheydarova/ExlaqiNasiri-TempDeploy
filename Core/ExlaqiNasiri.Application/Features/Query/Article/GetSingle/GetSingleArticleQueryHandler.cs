using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.Article;
using ExlaqiNasiri.Application.DTOs.Hadith;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Query.Article.GetSingle
{
    public class GetSingleArticleQueryHandler : IRequestHandler<GetSingleArticleQueryRequest, GetSingleQueryResponse<ArticleGetSingleDto>>
    {
        readonly IArticleService _service;

        public GetSingleArticleQueryHandler(IArticleService service)
        {
            _service = service;
        }

        public async Task<GetSingleQueryResponse<ArticleGetSingleDto>> Handle(GetSingleArticleQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.GetSingleAsync(request.Id);
            if (result.IsFailure) return new GetSingleQueryResponse<ArticleGetSingleDto> { StatusCode = (int)result.Error.StatusCode, Message = result.Error.Message };
            return new GetSingleQueryResponse<ArticleGetSingleDto>
            {
                Data = result.Value
            };
        }
    }
}
