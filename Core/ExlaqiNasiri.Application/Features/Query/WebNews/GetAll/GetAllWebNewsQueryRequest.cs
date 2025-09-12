using ExlaqiNasiri.Application.DTOs.WebNews;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Query.WebNews.GetAll
{
    public class GetAllWebNewsQueryRequest : IRequest<GetAllQueryResponse<WebNewsGetAllDto>>
    {
        public WebNewsFilterDto? Dto { get; set; }
    }
}
