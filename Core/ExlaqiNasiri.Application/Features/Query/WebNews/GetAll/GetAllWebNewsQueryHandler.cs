using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.HadithCategory;
using ExlaqiNasiri.Application.DTOs.WebNews;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Query.WebNews.GetAll
{  
    public class GetAllWebNewsQueryHandler : IRequestHandler<GetAllWebNewsQueryRequest, GetAllQueryResponse<WebNewsGetAllDto>>
    {
        readonly IWebNewsService _service;

        public GetAllWebNewsQueryHandler(IWebNewsService service)
        {
            _service = service;
        }

        public async Task<GetAllQueryResponse<WebNewsGetAllDto>> Handle(GetAllWebNewsQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.GetAllAsync(request.Dto);
            if (result.IsFailure) return new GetAllQueryResponse<WebNewsGetAllDto> { StatusCode = (int)result.Error.StatusCode, Message = result.Error.Message };
            return new GetAllQueryResponse<WebNewsGetAllDto>
            {
                Data = result.Value,
                TotalCount = result.Value.Count()
            };
        }
    }
}
