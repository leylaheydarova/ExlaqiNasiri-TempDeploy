using ExlaqiNasiri.Application.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Command.User.VerifyEmail
{
    public class VerifyEmailUserCommandRequest : IRequest<CommandResponse>
    {
        public string Email { get; set; }
        public string OtpCode { get; set; }
        public OtpPurpose Purpose { get; set; }
    }
}
