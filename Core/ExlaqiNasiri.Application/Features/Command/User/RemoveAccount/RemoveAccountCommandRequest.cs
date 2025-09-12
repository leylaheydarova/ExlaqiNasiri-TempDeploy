using ExlaqiNasiri.Application.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Command.User.RemoveAccount
{
    public class RemoveAccountCommandRequest : IRequest<CommandResponse>
    {
        public string UserIdOrEmail { get; set; }
        public UserType Type { get; set; }
    }
}
