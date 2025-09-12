using ExlaqiNasiri.Application.DTOs.WebNews;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Command.WebNews.Create
{
    public class CreateWebNewsCommandRequest : IRequest<CommandResponse>
    {
        public WebNewsCommandDto Dto { get; set; }
    }
}
