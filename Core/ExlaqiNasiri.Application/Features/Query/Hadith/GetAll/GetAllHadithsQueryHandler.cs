using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.Hadith;
using ExlaqiNasiri.Application.DTOs.HadithCategory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Query.Hadith.GetAll
{
    public class GetAllHadithsQueryHandler : IRequestHandler<GetAllHadithsQueryRequest, GetAllQueryResponse<HadithGetAllDto>>
    {
        readonly IHadithService _service;

        public GetAllHadithsQueryHandler(IHadithService service)
        {
            _service = service;
        }

        public async Task<GetAllQueryResponse<HadithGetAllDto>> Handle(GetAllHadithsQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.GetAllAsync(request.Dto);
            if (result.IsFailure) return new GetAllQueryResponse<HadithGetAllDto> { StatusCode = (int)result.Error.StatusCode, Message = result.Error.Message };
            return new GetAllQueryResponse<HadithGetAllDto>
            {
                Data = result.Value,
                TotalCount = result.Value.Count()
            };
        }
    }
}
