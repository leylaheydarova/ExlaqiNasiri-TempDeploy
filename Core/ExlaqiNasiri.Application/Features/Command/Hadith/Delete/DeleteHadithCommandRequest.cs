using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Command.Hadith.Delete
{
    public class DeleteHadithCommandRequest :  IRequest<CommandResponse>
    {
        public Guid Id { get; set; }
    }
}
