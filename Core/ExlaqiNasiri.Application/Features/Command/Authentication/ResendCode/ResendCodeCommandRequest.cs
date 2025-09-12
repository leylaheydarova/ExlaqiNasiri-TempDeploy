using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Authentication.ResendCode
{
    public class ResendCodeCommandRequest : IRequest<CommandResponse>
    {
        public string UserIdOrEmail { get; set; }
    }
}
