using ExlaqiNasiri.Application.Repositories.News;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Persistence.Repositories.News
{
    public class WebNewsWriteRepository : WriteRepository<WebNews>, IWebNewsWriteRepository
    {
        public WebNewsWriteRepository(ExlaqiNasiriDbContext context) : base(context)
        {
        }
    }
}
