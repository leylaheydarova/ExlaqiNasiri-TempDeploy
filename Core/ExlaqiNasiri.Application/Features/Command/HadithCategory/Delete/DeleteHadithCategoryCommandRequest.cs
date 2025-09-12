using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Command.HadithCategory.Delete
{
    public class DeleteHadithCategoryCommandRequest : IRequest<CommandResponse>
    {
        public Guid Id { get; set; }
    }
}
