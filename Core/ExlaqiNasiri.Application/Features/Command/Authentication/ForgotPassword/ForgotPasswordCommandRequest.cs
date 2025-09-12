using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Authentication.ForgotPassword
{
    public class ForgotPasswordCommandRequest : IRequest<CommandResponse>
    {
        public string Email { get; set; }
    }
}
