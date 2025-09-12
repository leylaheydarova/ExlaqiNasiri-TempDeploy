using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.Article;
using ExlaqiNasiri.Application.DTOs.Hadith;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Query.Article.GetAll
{
    public class GetAllArticleQueryHandler : IRequestHandler<GetAllArticleQueryRequest, GetAllQueryResponse<ArticleGetAllDto>>
    {
        readonly IArticleService _service;

        public GetAllArticleQueryHandler(IArticleService service)
        {
            _service = service;
        }

        public async Task<GetAllQueryResponse<ArticleGetAllDto>> Handle(GetAllArticleQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.GetAllAsync(request.Dto);
            if (result.IsFailure) return new GetAllQueryResponse<ArticleGetAllDto> { StatusCode = (int)result.Error.StatusCode, Message = result.Error.Message };
            return new GetAllQueryResponse<ArticleGetAllDto>
            {
                Data = result.Value,
                TotalCount = result.Value.Count()
            };
        }
    }
}
