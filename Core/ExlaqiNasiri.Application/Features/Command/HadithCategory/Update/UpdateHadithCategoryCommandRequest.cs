using ExlaqiNasiri.Application.DTOs.HadithCategory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Command.HadithCategory.Update
{
    public class UpdateHadithCategoryCommandRequest : IRequest<CommandResponse>
    {
        public Guid Id { get; set; }
        public HadithCategoryCommandDTO Dto { get; set; }
    }
}
