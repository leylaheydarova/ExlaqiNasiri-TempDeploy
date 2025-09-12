using ExlaqiNasiri.Application.DTOs.Authentication;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Command.Authentication.ResetPassword
{
    public class ResetPasswordCommandRequest : IRequest<CommandResponse>
    {
        public ResetPasswordDto dto { get; set; }
    }
}
