using ExlaqiNasiri.Application.DTOs.UserDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Command.User.ChangePassword
{
    public class ChangePasswordCommandRequest : CreateCommandRequest<PasswordUpdateDto>, IRequest<CommandResponse>
    {
    }
}
