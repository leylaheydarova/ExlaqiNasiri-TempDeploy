using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.WebUser;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.WebUser.GetAll
{
    public class GetAllWebUserQueryHandler : IRequestHandler<GetAllWebUserQueryRequest, GetAllQueryResponse<WebUserGetDto>>
    {
        readonly IWebUserService _webUserService;

        public GetAllWebUserQueryHandler(IWebUserService webUserService)
        {
            _webUserService = webUserService;
        }

        public async Task<GetAllQueryResponse<WebUserGetDto>> Handle(GetAllWebUserQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _webUserService.GetAllUserAsync(request.LastCreatedAt, request.Size);
            if (!result.IsSuccess) return new GetAllQueryResponse<WebUserGetDto>
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message
            };
            return new GetAllQueryResponse<WebUserGetDto>
            {
                Data = result.Value,
                TotalCount = result.Value.Count()
            };
        }
    }
}
