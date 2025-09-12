using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.Hadith;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.Hadith.GetSingle
{
    public class GetSingleHadithQueryHandler : IRequestHandler<GetSingleHadithQueryRequest, GetSingleQueryResponse<HadithGetSingleDto>>
    {
        readonly IHadithService _service;

        public GetSingleHadithQueryHandler(IHadithService service)
        {
            _service = service;
        }

        public async Task<GetSingleQueryResponse<HadithGetSingleDto>> Handle(GetSingleHadithQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.GetSingleAsync(request.Id);
            if (result.IsFailure) return new GetSingleQueryResponse<HadithGetSingleDto> { StatusCode = (int)result.Error.StatusCode, Message = result.Error.Message };
            return new GetSingleQueryResponse<HadithGetSingleDto>
            {
                Data = result.Value
            };
        }
    }
}
