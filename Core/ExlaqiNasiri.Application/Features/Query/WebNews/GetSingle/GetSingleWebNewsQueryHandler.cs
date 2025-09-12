using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.WebNews;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.WebNews.GetSingle
{
    public class GetSingleWebNewsQueryHandler : IRequestHandler<GetSingleWebNewsQueryRequest, GetSingleQueryResponse<WebNewsGetSingleDto>>
    {
        readonly IWebNewsService _service;

        public GetSingleWebNewsQueryHandler(IWebNewsService service)
        {
            _service = service;
        }

        public async Task<GetSingleQueryResponse<WebNewsGetSingleDto>> Handle(GetSingleWebNewsQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.GetSingleAsync(request.Id);
            if (result.IsFailure) return new GetSingleQueryResponse<WebNewsGetSingleDto> { StatusCode = (int)result.Error.StatusCode, Message = result.Error.Message };
            return new GetSingleQueryResponse<WebNewsGetSingleDto>
            {
                Data = result.Value
            };
        }
    }
}
