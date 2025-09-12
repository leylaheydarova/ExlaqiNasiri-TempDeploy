using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.WebUser;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.WebUser.GetSingle
{
    public class GetSingleWebUserQueryHandler : IRequestHandler<GetSingleWebUserQueryRequest, GetSingleQueryResponse<WebUserGetDto>>
    {
        readonly IWebUserService _webUserService;

        public GetSingleWebUserQueryHandler(IWebUserService webUserService)
        {
            _webUserService = webUserService;
        }

        public async Task<GetSingleQueryResponse<WebUserGetDto>> Handle(GetSingleWebUserQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _webUserService.GetSingleUserAsync(request.UserIdOrEmail);
            if (!result.IsSuccess) return new GetSingleQueryResponse<WebUserGetDto>
            {
                StatusCode = ((int)result.Error.StatusCode),
                Message = result.Error.Message
            };
            return new GetSingleQueryResponse<WebUserGetDto>
            {
                Data = result.Value
            };

        }
    }
}
