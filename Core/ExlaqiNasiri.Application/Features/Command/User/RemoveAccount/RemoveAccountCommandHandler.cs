using ExlaqiNasiri.Application.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Command.User.RemoveAccount
{
    public class RemoveAccountCommandHandler : IRequestHandler<RemoveAccountCommandRequest, CommandResponse>
    {
        private readonly IUserService _service;
        public RemoveAccountCommandHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<CommandResponse> Handle(RemoveAccountCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.RemoveAccountAsync(request.UserIdOrEmail, request.Type);

            if (result.IsSuccess)
            {
                return new CommandResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Account successfully deleted.",
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
