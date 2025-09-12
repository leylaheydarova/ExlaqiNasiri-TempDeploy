using ExlaqiNasiri.Application.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Command.User.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommandRequest, CommandResponse>
    {
        private readonly IUserService _service;
        public ChangePasswordCommandHandler(IUserService service)
        {
            _service = service;
        }
        public async Task<CommandResponse> Handle(ChangePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.ChangePasswordAsync(request.Dto);

            if (result.IsSuccess)
            {
                return new CommandResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Password successfully updated.",
                    Result = true
                };
            }

            return new CommandResponse
            {
                StatusCode = (int)result.Error.StatusCode,
                Message = result.Error.Message,
                Result = false
            };
        }
    }
}
